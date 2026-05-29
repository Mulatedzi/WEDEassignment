// Water For All - Website Project Proposal
// WEDE5020 Portfolio of Evidence - Part 1
//
// Colour Scheme: Ocean Blue (matching the website)
// Professional water-themed palette

using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DW = DocumentFormat.OpenXml.Drawing.Wordprocessing;
using A = DocumentFormat.OpenXml.Drawing;
using PIC = DocumentFormat.OpenXml.Drawing.Pictures;

namespace Docx;

public static class Program
{
    // ========================================================================
    // Colour Scheme — Ocean Blue (matching the website)
    // ========================================================================
    private static class Colors
    {
        public const string Primary = "0077B6";       // Primary blue — headings
        public const string Deep = "023E8A";          // Deep blue — accents
        public const string Teal = "00B4D8";          // Teal — highlights
        public const string Light = "90E0EF";         // Light blue — captions
        public const string Dark = "1A1A2E";           // Dark text
        public const string Mid = "444444";            // Medium text
        public const string Grey = "6C757D";           // Grey captions
        public const string Border = "CAF0F8";         // Table borders
        public const string TableHeader = "E8F4F8";    // Table header bg
        public const string White = "FFFFFF";
    }

    private const int A4W = 11906;
    private const int A4H = 16838;
    private const long A4WE = 7560000L;
    private const long A4HE = 10692000L;

    public static void Main(string[] args)
    {
        string outputPath = args.Length > 0 ? args[0] : "/mnt/agents/output/Water_For_All_Proposal.docx";
        string bgDir = "/mnt/agents/output/water-for-all-bg";
        Generate(outputPath, bgDir);
    }

    public static void Generate(string outputPath, string bgDir)
    {
        using var doc = WordprocessingDocument.Create(outputPath, WordprocessingDocumentType.Document);
        var mainPart = doc.AddMainDocumentPart();
        mainPart.Document = new Document(new Body());
        var body = mainPart.Document.Body!;

        AddStyles(mainPart);
        AddNumbering(mainPart);

        var coverBgId = AddImage(mainPart, Path.Combine(bgDir, "cover_bg.png"));
        var backBgId = AddImage(mainPart, Path.Combine(bgDir, "backcover_bg.png"));

        uint prId = 1;
        AddCoverSection(body, coverBgId, ref prId);
        AddTocSection(body);
        AddContentSection(doc, body, mainPart, bgDir, ref prId);
        AddBackcoverSection(body, backBgId, ref prId);

        SetUpdateFieldsOnOpen(mainPart);
        doc.Save();
    }

    // ========================================================================
    // Styles
    // ========================================================================
    private static void AddStyles(MainDocumentPart mainPart)
    {
        var sp = mainPart.AddNewPart<StyleDefinitionsPart>();
        sp.Styles = new Styles();

        sp.Styles.Append(new Style(
            new StyleName { Val = "Normal" },
            new StyleParagraphProperties(
                new SpacingBetweenLines { After = "200", Line = "312", LineRule = LineSpacingRuleValues.Auto }),
            new StyleRunProperties(
                new RunFonts { Ascii = "Calibri", HighAnsi = "Calibri", EastAsia = "Microsoft YaHei" },
                new FontSize { Val = "22" },
                new Color { Val = Colors.Dark })
        ) { Type = StyleValues.Paragraph, StyleId = "Normal", Default = true });

        sp.Styles.Append(CreateHeadingStyle("Heading1", "heading 1", 0, "36", Colors.Primary, "600", "240"));
        sp.Styles.Append(CreateHeadingStyle("Heading2", "heading 2", 1, "28", Colors.Deep, "400", "160"));
        sp.Styles.Append(CreateHeadingStyle("Heading3", "heading 3", 2, "24", Colors.Mid, "280", "120"));

        sp.Styles.Append(new Style(
            new StyleName { Val = "Caption" }, new BasedOn { Val = "Normal" },
            new StyleParagraphProperties(
                new Justification { Val = JustificationValues.Center },
                new SpacingBetweenLines { Before = "60", After = "320" }),
            new StyleRunProperties(new Color { Val = Colors.Grey }, new FontSize { Val = "20" })
        ) { Type = StyleValues.Paragraph, StyleId = "Caption" });

        sp.Styles.Append(CreateTocStyle("TOC1", "toc 1", true, "0", "200"));
        sp.Styles.Append(CreateTocStyle("TOC2", "toc 2", false, "360", "60"));
    }

    private static Style CreateHeadingStyle(string id, string name, int level,
        string fontSize, string color, string spaceBefore, string spaceAfter)
    {
        return new Style(
            new StyleName { Val = name }, new BasedOn { Val = "Normal" },
            new StyleParagraphProperties(
                new KeepNext(), new KeepLines(),
                new SpacingBetweenLines { Before = spaceBefore, After = spaceAfter },
                new OutlineLevel { Val = level }),
            new StyleRunProperties(
                new Bold(), new FontSize { Val = fontSize },
                new RunFonts { Ascii = "Calibri", HighAnsi = "Calibri", EastAsia = "Microsoft YaHei" },
                new Color { Val = color })
        ) { Type = StyleValues.Paragraph, StyleId = id };
    }

    private static Style CreateTocStyle(string id, string name, bool bold, string indent, string before)
    {
        var rpr = new StyleRunProperties(new Color { Val = bold ? Colors.Dark : Colors.Mid });
        if (bold) rpr.Append(new Bold());
        return new Style(
            new StyleName { Val = name }, new BasedOn { Val = "Normal" },
            new StyleParagraphProperties(
                new Tabs(new TabStop { Val = TabStopValues.Right, Leader = TabStopLeaderCharValues.Dot, Position = 9350 }),
                new SpacingBetweenLines { Before = before, After = "60" },
                new Indentation { Left = indent }),
            rpr
        ) { Type = StyleValues.Paragraph, StyleId = id };
    }

    // ========================================================================
    // Cover Page
    // ========================================================================
    private static void AddCoverSection(Body body, string coverBgId, ref uint prId)
    {
        body.Append(new Paragraph(new Run(CreateFloatingBackground(coverBgId, prId++, "CoverBg"))));
        body.Append(new Paragraph(new ParagraphProperties(new SpacingBetweenLines { Before = "5500" }), new Run()));

        body.Append(new Paragraph(
            new ParagraphProperties(
                new Justification { Val = JustificationValues.Left },
                new Indentation { Left = "1200", Right = "1200" },
                new SpacingBetweenLines { After = "200" }),
            new Run(new RunProperties(
                    new FontSize { Val = "72" },
                    new Color { Val = Colors.Deep },
                    new Spacing { Val = 30 }),
                new Text("Water For All"))));

        body.Append(new Paragraph(
            new ParagraphProperties(
                new Justification { Val = JustificationValues.Left },
                new Indentation { Left = "1200", Right = "1200" },
                new SpacingBetweenLines { After = "400" }),
            new Run(new RunProperties(
                    new FontSize { Val = "36" },
                    new Color { Val = Colors.Primary },
                    new Spacing { Val = 20 }),
                new Text("Website Project Proposal"))));

        body.Append(new Paragraph(
            new ParagraphProperties(
                new Justification { Val = JustificationValues.Left },
                new Indentation { Left = "1200", Right = "1200" },
                new SpacingBetweenLines { After = "3000" }),
            new Run(new RunProperties(
                    new FontSize { Val = "22" },
                    new Color { Val = Colors.Mid }),
                new Text("WEDE5020 — Web Development Portfolio of Evidence"))));

        body.Append(new Paragraph(
            new ParagraphProperties(new Indentation { Left = "1200" }),
            new Run(new RunProperties(new FontSize { Val = "20" }, new Color { Val = Colors.Grey }),
                new Text("Submitted: April 2025"))));

        body.Append(new Paragraph(new ParagraphProperties(new SectionProperties(
            new TitlePage(),
            new SectionType { Val = SectionMarkValues.NextPage },
            new PageSize { Width = (UInt32Value)(uint)A4W, Height = (UInt32Value)(uint)A4H },
            new PageMargin { Top = 0, Right = 0, Bottom = 0, Left = 0, Header = 0, Footer = 0 }))));
    }

    // ========================================================================
    // Table of Contents
    // ========================================================================
    private static void AddTocSection(Body body)
    {
        body.Append(CreateHeading1("Table of Contents", "_Toc000"));

        body.Append(new Paragraph(
            new ParagraphProperties(new SpacingBetweenLines { After = "300" }),
            new Run(new RunProperties(new Color { Val = Colors.Grey }, new FontSize { Val = "18" }),
                new Text("Right-click the TOC and select \"Update Field\" to refresh page numbers"))));

        body.Append(new Paragraph(
            new Run(new FieldChar { FieldCharType = FieldCharValues.Begin }),
            new Run(new FieldCode(" TOC \\o \"1-3\" \\h \\z \\u ") { Space = SpaceProcessingModeValues.Preserve }),
            new Run(new FieldChar { FieldCharType = FieldCharValues.Separate })));

        string[,] toc = {
            { "1. Organisation Overview", "1", "3" },
            { "2. Website Goals and Objectives", "1", "4" },
            { "3. Current Website Analysis", "1", "5" },
            { "4. Proposed Website Features", "1", "5" },
            { "5. Design and User Experience", "1", "7" },
            { "6. Technical Requirements", "1", "9" },
            { "7. Timeline and Milestones", "1", "10" },
            { "8. Budget", "1", "11" },
            { "9. References", "1", "12" },
        };
        for (int i = 0; i < toc.GetLength(0); i++)
            body.Append(new Paragraph(
                new ParagraphProperties(new ParagraphStyleId { Val = $"TOC{toc[i, 1]}" }),
                new Run(new Text(toc[i, 0])), new Run(new TabChar()), new Run(new Text(toc[i, 2]))));

        body.Append(new Paragraph(new Run(new FieldChar { FieldCharType = FieldCharValues.End })));

        body.Append(new Paragraph(new ParagraphProperties(new SectionProperties(
            new SectionType { Val = SectionMarkValues.NextPage },
            new PageSize { Width = (UInt32Value)(uint)A4W, Height = (UInt32Value)(uint)A4H },
            new PageMargin { Top = 1800, Right = 1440, Bottom = 1440, Left = 1440, Header = 720, Footer = 720 }))));
    }

    // ========================================================================
    // Main Content
    // ========================================================================
    private static void AddContentSection(WordprocessingDocument doc, Body body,
        MainDocumentPart mainPart, string bgDir, ref uint prId)
    {
        // Header
        var headerPart = mainPart.AddNewPart<HeaderPart>();
        var headerId = mainPart.GetIdOfPart(headerPart);
        var bodyBgPath = Path.Combine(bgDir, "body_bg.png");
        if (File.Exists(bodyBgPath))
        {
            var headerImagePart = headerPart.AddImagePart(ImagePartType.Png);
            using (var stream = new FileStream(bodyBgPath, FileMode.Open))
                headerImagePart.FeedData(stream);
            var headerImageId = headerPart.GetIdOfPart(headerImagePart);
            headerPart.Header = new Header(
                new Paragraph(new Run(CreateFloatingBackground(headerImageId, prId++, "BodyBg"))),
                new Paragraph(
                    new ParagraphProperties(new Justification { Val = JustificationValues.Right }),
                    new Run(new RunProperties(new FontSize { Val = "18" }, new Color { Val = Colors.Grey }),
                        new Text("Water For All — Website Project Proposal"))));
        }
        else
        {
            headerPart.Header = new Header(new Paragraph(
                new ParagraphProperties(new Justification { Val = JustificationValues.Right }),
                new Run(new RunProperties(new FontSize { Val = "18" }, new Color { Val = Colors.Grey }),
                    new Text("Water For All — Website Project Proposal"))));
        }

        // Footer
        var footerPart = mainPart.AddNewPart<FooterPart>();
        var footerId = mainPart.GetIdOfPart(footerPart);
        var fp = new Paragraph(new ParagraphProperties(new Justification { Val = JustificationValues.Center }));
        fp.Append(new Run(new RunProperties(new FontSize { Val = "18" }, new Color { Val = Colors.Grey }),
            new FieldChar { FieldCharType = FieldCharValues.Begin }));
        fp.Append(new Run(new RunProperties(new FontSize { Val = "18" }, new Color { Val = Colors.Grey }),
            new FieldCode(" PAGE ") { Space = SpaceProcessingModeValues.Preserve }));
        fp.Append(new Run(new RunProperties(new FontSize { Val = "18" }, new Color { Val = Colors.Grey }),
            new FieldChar { FieldCharType = FieldCharValues.Separate }));
        fp.Append(new Run(new RunProperties(new FontSize { Val = "18" }, new Color { Val = Colors.Grey }),
            new Text("1")));
        fp.Append(new Run(new RunProperties(new FontSize { Val = "18" }, new Color { Val = Colors.Grey }),
            new FieldChar { FieldCharType = FieldCharValues.End }));
        footerPart.Footer = new Footer(fp);

        // --- SECTION 1: Organisation Overview ---
        body.Append(CreateHeading1("1. Organisation Overview", "_Toc001"));
        
        body.Append(CreateHeading2("1.1 Organisation Name"));
        body.Append(CreateParagraph("Water For All is a registered non-profit organisation (NPO) operating in South Africa. The organisation was formally established in 2019 and is dedicated to addressing water scarcity and providing sustainable access to clean, safe drinking water for underserved communities across the country."));

        body.Append(CreateHeading2("1.2 Brief History"));
        body.Append(CreateParagraph("Water For All was founded by a group of environmental engineers and community activists who recognised the urgent need for clean water solutions in rural South Africa. The founding team, led by Thabo Sithole, witnessed first-hand the devastating health and economic impacts that unsafe water sources had on communities in the Eastern Cape."));
        body.Append(CreateParagraph("Beginning with a single pilot project in 2019 that served approximately 500 residents, the organisation has grown steadily. By 2022, Water For All had expanded operations into KwaZulu-Natal and Limpopo. As of 2025, the organisation operates 320 water purification stations across four provinces and has brought clean water access to over 75,000 people."));
        body.Append(CreateParagraph("The organisation has been recognised by the Department of Water and Sanitation for its innovative community-centred approach and has secured partnerships with several corporate sponsors including major South African banks and mining companies fulfilling their CSI mandates."));

        body.Append(CreateHeading2("1.3 Mission and Vision Statements"));
        body.Append(CreateParagraph("Mission: To provide sustainable access to clean, safe drinking water for underserved communities across South Africa through innovative technology, education, and community-driven partnerships that create lasting change."));
        body.Append(CreateParagraph("Vision: A South Africa where every person, regardless of geography or economic status, has reliable access to clean drinking water by 2035 — creating healthier communities, stronger local economies, and a more equitable society."));

        body.Append(CreateHeading2("1.4 Target Audience"));
        body.Append(CreateParagraph("The primary target audience for Water For All's website includes:"));
        body.Append(CreateBulletItem("Underserved Communities: Residents in rural and peri-urban areas seeking access to clean water solutions and information about our services."));
        body.Append(CreateBulletItem("Potential Volunteers: Individuals aged 18-45 interested in contributing their time and skills to water-related community projects."));
        body.Append(CreateBulletItem("Corporate Partners: Businesses and organisations looking to fulfil corporate social investment (CSI) objectives through meaningful water projects."));
        body.Append(CreateBulletItem("Government Stakeholders: Municipal and provincial officials interested in public-private partnerships for water infrastructure."));
        body.Append(CreateBulletItem("Donors and Funders: Philanthropic organisations, trusts, and individual donors who want to contribute financially to water access initiatives."));
        body.Append(CreateBulletItem("International NGOs: Global organisations seeking local implementation partners for water-related development programmes."));

        // --- SECTION 2: Website Goals and Objectives ---
        body.Append(CreateHeading1("2. Website Goals and Objectives", "_Toc002"));

        body.Append(CreateHeading2("2.1 Primary Goals"));
        body.Append(CreateBulletItem("Increase Awareness: Raise public awareness about water scarcity issues in South Africa and position Water For All as a leading organisation addressing this challenge."));
        body.Append(CreateBulletItem("Generate Leads: Attract enquiries from communities needing services, potential volunteers, and corporate sponsors through clear calls-to-action."));
        body.Append(CreateBulletItem("Provide Information: Serve as a comprehensive information hub about our services, impact, and ways to get involved."));
        body.Append(CreateBulletItem("Build Credibility: Establish trust through transparent reporting of our impact, financial stewardship, and operational results."));
        body.Append(CreateBulletItem("Facilitate Engagement: Enable multiple pathways for engagement including volunteering, sponsorship, donations, and service requests."));

        body.Append(CreateHeading2("2.2 Key Performance Indicators (KPIs)"));
        body.Append(CreateParagraph("The following KPIs will be used to measure the success of the website:"));
        body.Append(CreateKpiTable());

        // --- SECTION 3: Current Website Analysis ---
        body.Append(CreateHeading1("3. Current Website Analysis", "_Toc003"));
        body.Append(CreateParagraph("Water For All is a newly established organisation that has not previously had a dedicated website. The organisation has relied on social media platforms (Facebook and Instagram) and word-of-mouth communication to reach stakeholders."));
        body.Append(CreateParagraph("As there is no existing website to analyse, this proposal presents a greenfield project. The design and development will be informed by best practices in non-profit website design, competitor analysis of similar organisations (such as WaterAid and The Water Project), and direct feedback from our community partners about the information they need most."));
        body.Append(CreateParagraph("The absence of a current website represents both an opportunity and a challenge. We have the freedom to build a modern, responsive website from the ground up without legacy constraints, but we also need to establish our digital presence from scratch."));

        // --- SECTION 4: Proposed Website Features ---
        body.Append(CreateHeading1("4. Proposed Website Features and Functionality", "_Toc004"));

        body.Append(CreateHeading2("4.1 Common Page Structure"));
        body.Append(CreateParagraph("The website will consist of a minimum of five core pages, each serving a distinct purpose:"));

        body.Append(CreateHeading3("Homepage (index.html)"));
        body.Append(CreateParagraph("The homepage serves as the primary entry point and will feature a full-width hero section with a compelling water-themed image, the organisation tagline, and clear calls-to-action. Below the hero, the page will display impact statistics (communities served, people with water access, water stations installed, and active volunteers), followed by a summary of our three core services and a mission statement section."));

        body.Append(CreateHeading3("About Us (about.html)"));
        body.Append(CreateParagraph("This page will present the organisation's history, mission, vision, and core values. It will include a narrative of our founding story, growth trajectory, and the team behind the organisation. The page will feature photographs from the field and team portraits to humanise the organisation."));

        body.Append(CreateHeading3("Services (services.html)"));
        body.Append(CreateParagraph("The services page will provide detailed information about Water For All's core offerings including water purification systems, conservation education, community outreach, emergency water response, water quality testing, and corporate partnerships. Each service will be presented with descriptive imagery and clear explanations."));

        body.Append(CreateHeading3("Enquiry (enquiry.html)"));
        body.Append(CreateParagraph("The enquiry form allows visitors to submit questions about services, express interest in volunteering, or explore sponsorship opportunities. The form includes fields for enquiry type (service, volunteer, or sponsorship), full name, email, phone, organisation, province, and a message field. As an NPO, the form enables visitors to enquire about volunteering or becoming a sponsor."));

        body.Append(CreateHeading3("Contact (contact.html)"));
        body.Append(CreateParagraph("The contact page displays full contact information for all three office locations (Cape Town, Durban, and Polokwane), a contact form for general messages, office hours, emergency water hotline details, and a map showing the locations of our offices. The page also includes links to social media profiles."));

        body.Append(CreateHeading2("4.2 File and Folder Structure"));
        body.Append(CreateFolderStructureTable());

        // --- SECTION 5: Design and User Experience ---
        body.Append(CreateHeading1("5. Design and User Experience", "_Toc005"));

        body.Append(CreateHeading2("5.1 Colour Scheme"));
        body.Append(CreateParagraph("The colour palette is inspired by water and nature, creating a sense of trust, cleanliness, and environmental consciousness:"));
        body.Append(CreateColorTable());
        body.Append(CreateParagraph("The primary blue (#0077B6) evokes trust and water, while the deep blue (#023E8A) provides gravitas for headings. The teal accent (#00B4D8) adds energy and draws attention to calls-to-action. Light blue tones (#90E0EF, #CAF0F8) are used for backgrounds and subtle highlights. Dark text (#1A1A2E) ensures excellent readability."));

        body.Append(CreateHeading2("5.2 Typography"));
        body.Append(CreateParagraph("The website uses a clean, modern sans-serif font stack optimised for readability across all devices:"));
        body.Append(CreateBulletItem("Primary Font: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif — a system font stack that loads quickly and renders consistently across platforms."));
        body.Append(CreateBulletItem("Headings: Bold weight, with H1 at 2.5rem (40px), H2 at 2rem (32px), and H3 at 1.5rem (24px). Heading colours use the deep blue (#023E8A) for strong visual hierarchy."));
        body.Append(CreateBulletItem("Body Text: Regular weight at 16px with 1.6 line height for comfortable reading. Body text colour is dark (#1A1A2E) on light backgrounds."));
        body.Append(CreateBulletItem("Captions and Labels: Smaller text at 0.9rem using the grey colour (#6C757D) for secondary information."));

        body.Append(CreateHeading2("5.3 Layout and Design"));
        body.Append(CreateParagraph("The website employs a responsive grid-based layout that adapts seamlessly from mobile to desktop screens:"));
        body.Append(CreateBulletItem("Container: Maximum width of 1200px centred on the page with 90% width and 15px padding on mobile."));
        body.Append(CreateBulletItem("Header: Sticky navigation bar with logo on the left and horizontal menu on the right. A hamburger menu appears on screens below 768px."));
        body.Append(CreateBulletItem("Hero Section: Full-viewport height with a background image, gradient overlay for text readability, centred content, and call-to-action buttons."));
        body.Append(CreateBulletItem("Content Sections: Alternating light and dark backgrounds to create visual rhythm and separate content areas."));
        body.Append(CreateBulletItem("Cards: Rounded corners (12px), subtle shadows, and hover lift effects for service and feature cards."));
        body.Append(CreateBulletItem("Footer: Four-column grid containing brand information, quick links, services links, and contact details."));

        body.Append(CreateHeading2("5.4 User Experience Considerations"));
        body.Append(CreateBulletItem("Accessibility: Skip-to-content link for keyboard users, semantic HTML5 tags (header, nav, main, section, article, footer), ARIA labels where appropriate, and sufficient colour contrast ratios (all text meets WCAG AA standards)."));
        body.Append(CreateBulletItem("Navigation: Clear, consistent navigation across all pages with active page highlighting and smooth hover animations."));
        body.Append(CreateBulletItem("Mobile Responsiveness: Fully responsive design with breakpoints at 1024px, 768px, and 480px. Grids collapse to single columns on mobile, and the navigation converts to a hamburger menu."));
        body.Append(CreateBulletItem("Loading Performance: System font stack eliminates external font loading. Images are optimised and CSS/JS files are minified for fast page loads."));
        body.Append(CreateBulletItem("Interactive Elements: Animated statistics counters, hover effects on cards and buttons, form validation with clear error messages, and a scroll-to-top button."));

        body.Append(CreateHeading2("5.5 Low-Fidelity Wireframes"));
        body.Append(CreateParagraph("The following wireframe descriptions outline the layout and information hierarchy for each key page:"));
        body.Append(CreateBulletItem("Homepage Wireframe: [Top to Bottom] Sticky header with logo and nav → Full-screen hero with image overlay, heading, tagline, and two CTA buttons → Dark impact stats section with 4 counter boxes → Light features section with 3 service cards → Mission statement section → CTA banner → Footer with 4 columns."));
        body.Append(CreateBulletItem("About Wireframe: Header → Blue hero banner with page title → Two-column layout: image left, history text right → Mission/vision section with two-column layout → Team section with grid of member cards → CTA banner → Footer."));
        body.Append(CreateBulletItem("Services Wireframe: Header → Blue hero banner → Grid of 3 service cards with images → Grid of 6 additional programme cards → Dark stats section → CTA banner → Footer."));
        body.Append(CreateBulletItem("Enquiry Wireframe: Header → Blue hero banner → Two-column layout: enquiry type descriptions left, form right → FAQ accordion section → CTA banner → Footer."));
        body.Append(CreateBulletItem("Contact Wireframe: Header → Blue hero banner → Two-column layout: contact info left, form right → 3-column office locations grid → Map placeholder → Social media section → Footer."));

        // --- SECTION 6: Technical Requirements ---
        body.Append(CreateHeading1("6. Technical Requirements", "_Toc006"));

        body.Append(CreateHeading2("6.1 Hosting and Domain"));
        body.Append(CreateBulletItem("Domain Name: waterforall.org.za — registered through a South African domain registrar."));
        body.Append(CreateBulletItem("Hosting Provider: Shared hosting plan with a reputable South African web hosting provider (e.g., Afrihost, Xneelo, or HostAfrica) offering 99.9% uptime, SSL certificates, and sufficient bandwidth for anticipated traffic."));
        body.Append(CreateBulletItem("SSL Certificate: Let's Encrypt free SSL certificate for HTTPS encryption, or a commercial SSL certificate if required by corporate partners."));

        body.Append(CreateHeading2("6.2 Programming Languages and Technologies"));
        body.Append(CreateTechTable());

        body.Append(CreateHeading2("6.3 Browser Compatibility"));
        body.Append(CreateParagraph("The website will be tested and fully functional on the following browsers: Google Chrome (latest 2 versions), Mozilla Firefox (latest 2 versions), Apple Safari (latest 2 versions), Microsoft Edge (latest 2 versions), and Opera (latest version). Mobile compatibility will be verified on iOS Safari and Android Chrome."));

        // --- SECTION 7: Timeline ---
        body.Append(CreateHeading1("7. Timeline and Milestones", "_Toc007"));
        body.Append(CreateParagraph("The website development project follows a realistic timeline aligned with the academic submission dates for WEDE5020:"));
        body.Append(CreateTimelineTable());

        // --- SECTION 8: Budget ---
        body.Append(CreateHeading1("8. Budget", "_Toc008"));
        body.Append(CreateParagraph("The following budget estimates are based on research into current market rates for web development services and hosting in South Africa (2025):"));
        body.Append(CreateBudgetTable());
        body.Append(CreateParagraph("Note: As this is an academic project, actual costs may vary. Labour costs are estimated based on freelance developer rates in South Africa. Ongoing costs are annual estimates."));

        // --- SECTION 9: References ---
        body.Append(CreateHeading1("9. References", "_Toc009"));
        body.Append(CreateReference("1.", "Department of Water and Sanitation (2024). National Water and Sanitation Master Plan. Available at: https://www.dws.gov.za [Accessed 15 April 2025]."));
        body.Append(CreateReference("2.", "Stats SA (2024). General Household Survey 2023. Pretoria: Statistics South Africa."));
        body.Append(CreateReference("3.", "WaterAid (2024). The Water Effect. Available at: https://www.wateraid.org [Accessed 10 April 2025]."));
        body.Append(CreateReference("4.", "World Health Organization (2023). Guidelines for Drinking-water Quality, 4th Edition. Geneva: WHO Press."));
        body.Append(CreateReference("5.", "MDN Web Docs (2025). HTML: HyperText Markup Language. Available at: https://developer.mozilla.org/en-US/docs/Web/HTML [Accessed 12 April 2025]."));
        body.Append(CreateReference("6.", "MDN Web Docs (2025). CSS: Cascading Style Sheets. Available at: https://developer.mozilla.org/en-US/docs/Web/CSS [Accessed 12 April 2025]."));
        body.Append(CreateReference("7.", "MDN Web Docs (2025). JavaScript. Available at: https://developer.mozilla.org/en-US/docs/Web/JavaScript [Accessed 12 April 2025]."));
        body.Append(CreateReference("8.", "W3C (2024). HTML5 Specification. Available at: https://www.w3.org/TR/html5/ [Accessed 10 April 2025]."));
        body.Append(CreateReference("9.", "WCAG 2.1 (2018). Web Content Accessibility Guidelines 2.1. W3C Recommendation. Available at: https://www.w3.org/WAI/WCAG21/quickref/ [Accessed 14 April 2025]."));
        body.Append(CreateReference("10.", "Nielsen Norman Group (2024). 10 Usability Heuristics for User Interface Design. Available at: https://www.nngroup.com/articles/ten-usability-heuristics/ [Accessed 14 April 2025]."));
        body.Append(CreateReference("11.", "Mozilla (2025). Responsive Design Basics. Available at: https://developer.mozilla.org/en-US/docs/Learn/CSS/CSS_layout/Responsive_Design [Accessed 12 April 2025]."));
        body.Append(CreateReference("12.", "Afrihost (2025). Web Hosting Packages. Available at: https://www.afrihost.com [Accessed 15 April 2025]."));

        // Content section break
        body.Append(new Paragraph(new ParagraphProperties(new SectionProperties(
            new HeaderReference { Type = HeaderFooterValues.Default, Id = headerId },
            new FooterReference { Type = HeaderFooterValues.Default, Id = footerId },
            new PageSize { Width = (UInt32Value)(uint)A4W, Height = (UInt32Value)(uint)A4H },
            new PageMargin { Top = 1800, Right = 1440, Bottom = 1440, Left = 1440, Header = 720, Footer = 720 }))));
    }

    // ========================================================================
    // Back Cover
    // ========================================================================
    private static void AddBackcoverSection(Body body, string backBgId, ref uint prId)
    {
        body.Append(new Paragraph(new Run(CreateFloatingBackground(backBgId, prId++, "BackBg"))));

        body.Append(new Paragraph(
            new ParagraphProperties(new SpacingBetweenLines { Before = "7000" },
                new Justification { Val = JustificationValues.Center }),
            new Run(new RunProperties(new FontSize { Val = "48" }, new Bold(),
                new Color { Val = Colors.Deep }),
                new Text("Water For All"))));

        body.Append(new Paragraph(
            new ParagraphProperties(new SpacingBetweenLines { Before = "400" },
                new Justification { Val = JustificationValues.Center }),
            new Run(new RunProperties(new FontSize { Val = "22" }, new Color { Val = Colors.Primary }),
                new Text("Clean Water for Every Community"))));

        body.Append(new Paragraph(
            new ParagraphProperties(new SpacingBetweenLines { Before = "600" },
                new Justification { Val = JustificationValues.Center }),
            new Run(new RunProperties(new FontSize { Val = "18" }, new Color { Val = Colors.Grey }),
                new Text("info@waterforall.org.za  |  +27 21 555 0140"))));

        body.Append(new Paragraph(
            new ParagraphProperties(new SpacingBetweenLines { Before = "200" },
                new Justification { Val = JustificationValues.Center }),
            new Run(new RunProperties(new FontSize { Val = "16" }, new Color { Val = Colors.Grey }),
                new Text("\u00A9 2025 Water For All. All rights reserved."))));

        body.Append(new SectionProperties(
            new PageSize { Width = (UInt32Value)(uint)A4W, Height = (UInt32Value)(uint)A4H },
            new PageMargin { Top = 0, Right = 0, Bottom = 0, Left = 0, Header = 0, Footer = 0 }));
    }

    // ========================================================================
    // Factory Helpers
    // ========================================================================
    private static int _bookmarkId = 0;

    private static Paragraph CreateHeading1(string text, string bookmarkName)
    {
        int id = ++_bookmarkId;
        return new Paragraph(
            new ParagraphProperties(new ParagraphStyleId { Val = "Heading1" }),
            new BookmarkStart { Id = id.ToString(), Name = bookmarkName },
            new Run(new Text(text)),
            new BookmarkEnd { Id = id.ToString() });
    }

    private static Paragraph CreateHeading2(string text)
    {
        return new Paragraph(
            new ParagraphProperties(new ParagraphStyleId { Val = "Heading2" }),
            new Run(new Text(text)));
    }

    private static Paragraph CreateHeading3(string text)
    {
        return new Paragraph(
            new ParagraphProperties(new ParagraphStyleId { Val = "Heading3" }),
            new Run(new Text(text)));
    }

    private static Paragraph CreateParagraph(string text)
    {
        return new Paragraph(new Run(new Text(text)));
    }

    private static Paragraph CreateBulletItem(string text)
    {
        return new Paragraph(
            new ParagraphProperties(
                new NumberingProperties(new NumberingLevelReference { Val = 0 }, new NumberingId { Val = 1 })),
            new Run(new Text(text)));
    }

    private static Paragraph CreateReference(string number, string text)
    {
        return new Paragraph(
            new ParagraphProperties(new SpacingBetweenLines { After = "120" }),
            new Run(new RunProperties(new Color { Val = Colors.Mid }, new FontSize { Val = "20" }),
                new Text(number + " " + text) { Space = SpaceProcessingModeValues.Preserve }));
    }

    // ========================================================================
    // Tables
    // ========================================================================
    private static Table CreateKpiTable()
    {
        string[] w = { "2500", "3500", "4000" };
        var tbl = new Table(CreateTableProperties(),
            new TableGrid(new GridColumn { Width = w[0] }, new GridColumn { Width = w[1] }, new GridColumn { Width = w[2] }));
        tbl.Append(CreateTableRow(true, w, "KPI", "Target", "Measurement Method"));
        tbl.Append(CreateTableRow(false, w, "Website Traffic", "1,000 unique visitors/month by Month 6", "Google Analytics"));
        tbl.Append(CreateTableRow(false, w, "Enquiry Form Submissions", "30 enquiries/month by Month 6", "Form submission tracking"));
        tbl.Append(CreateTableRow(false, w, "Volunteer Sign-ups", "15 new volunteers/month", "Enquiry form data"));
        tbl.Append(CreateTableRow(false, w, "Corporate Partnership Leads", "5 qualified leads/month", "Enquiry form + email tracking"));
        tbl.Append(CreateTableRow(false, w, "Average Session Duration", ">2 minutes", "Google Analytics"));
        tbl.Append(CreateTableRow(false, w, "Bounce Rate", "<50%", "Google Analytics"));
        tbl.Append(CreateTableRow(false, w, "Mobile Traffic Ratio", ">60%", "Google Analytics device report"));
        return tbl;
    }

    private static Table CreateFolderStructureTable()
    {
        string[] w = { "3000", "7000" };
        var tbl = new Table(CreateTableProperties(),
            new TableGrid(new GridColumn { Width = w[0] }, new GridColumn { Width = w[1] }));
        tbl.Append(CreateTableRow(true, w, "Folder/File", "Description"));
        tbl.Append(CreateTableRow(false, w, "index.html", "Homepage with hero, stats, features, and CTA"));
        tbl.Append(CreateTableRow(false, w, "about.html", "About Us with history, mission, vision, and team"));
        tbl.Append(CreateTableRow(false, w, "services.html", "Services with detailed service cards and programmes"));
        tbl.Append(CreateTableRow(false, w, "enquiry.html", "Enquiry form for services, volunteering, and sponsorship"));
        tbl.Append(CreateTableRow(false, w, "contact.html", "Contact with 3 locations, form, and map"));
        tbl.Append(CreateTableRow(false, w, "css/style.css", "Main stylesheet with all layout and design rules"));
        tbl.Append(CreateTableRow(false, w, "js/main.js", "JavaScript for navigation, forms, and interactions"));
        tbl.Append(CreateTableRow(false, w, "images/", "Folder containing all website images"));
        return tbl;
    }

    private static Table CreateColorTable()
    {
        string[] w = { "2500", "2000", "5500" };
        var tbl = new Table(CreateTableProperties(),
            new TableGrid(new GridColumn { Width = w[0] }, new GridColumn { Width = w[1] }, new GridColumn { Width = w[2] }));
        tbl.Append(CreateTableRow(true, w, "Colour", "Hex Code", "Usage"));
        tbl.Append(CreateTableRow(false, w, "Primary Blue", "#0077B6", "Headings, links, primary buttons"));
        tbl.Append(CreateTableRow(false, w, "Deep Blue", "#023E8A", "Dark sections, footer, H2 headings"));
        tbl.Append(CreateTableRow(false, w, "Teal Accent", "#00B4D8", "CTA buttons, hover states, highlights"));
        tbl.Append(CreateTableRow(false, w, "Light Blue", "#90E0EF", "Hover effects, secondary accents"));
        tbl.Append(CreateTableRow(false, w, "Pale Blue", "#CAF0F8", "Card backgrounds, light sections"));
        tbl.Append(CreateTableRow(false, w, "Dark Text", "#1A1A2E", "Body text, headings on light backgrounds"));
        tbl.Append(CreateTableRow(false, w, "Grey Text", "#6C757D", "Captions, secondary information"));
        return tbl;
    }

    private static Table CreateTechTable()
    {
        string[] w = { "3500", "6500" };
        var tbl = new Table(CreateTableProperties(),
            new TableGrid(new GridColumn { Width = w[0] }, new GridColumn { Width = w[1] }));
        tbl.Append(CreateTableRow(true, w, "Technology", "Purpose"));
        tbl.Append(CreateTableRow(false, w, "HTML5", "Semantic page structure and content markup"));
        tbl.Append(CreateTableRow(false, w, "CSS3", "Styling, responsive layout, animations, and visual design"));
        tbl.Append(CreateTableRow(false, w, "JavaScript (Vanilla)", "Form validation, mobile navigation, interactive elements"));
        tbl.Append(CreateTableRow(false, w, "No external frameworks", "Lightweight, fast-loading, dependency-free implementation"));
        return tbl;
    }

    private static Table CreateTimelineTable()
    {
        string[] w = { "2200", "5300", "2500" };
        var tbl = new Table(CreateTableProperties(),
            new TableGrid(new GridColumn { Width = w[0] }, new GridColumn { Width = w[1] }, new GridColumn { Width = w[2] }));
        tbl.Append(CreateTableRow(true, w, "Phase", "Activities", "Duration"));
        tbl.Append(CreateTableRow(false, w, "Phase 1: Planning", "Requirements analysis, content gathering, sitemap creation", "Week 1-2"));
        tbl.Append(CreateTableRow(false, w, "Phase 2: Design", "Wireframing, colour scheme finalisation, typography selection", "Week 3-4"));
        tbl.Append(CreateTableRow(false, w, "Phase 3: Development", "HTML structure, CSS styling, JavaScript functionality", "Week 5-8"));
        tbl.Append(CreateTableRow(false, w, "Phase 4: Content", "Image optimisation, content integration, copy refinement", "Week 7-9"));
        tbl.Append(CreateTableRow(false, w, "Phase 5: Testing", "Cross-browser testing, mobile responsiveness, form validation", "Week 9-10"));
        tbl.Append(CreateTableRow(false, w, "Phase 6: Deployment", "Hosting setup, domain configuration, SSL, go-live", "Week 11"));
        tbl.Append(CreateTableRow(false, w, "Phase 7: Maintenance", "Ongoing updates, content refreshes, performance monitoring", "Ongoing"));
        return tbl;
    }

    private static Table CreateBudgetTable()
    {
        string[] w = { "6500", "3500" };
        var tbl = new Table(CreateTableProperties(),
            new TableGrid(new GridColumn { Width = w[0] }, new GridColumn { Width = w[1] }));
        tbl.Append(CreateTableRow(true, w, "Item", "Estimated Cost (ZAR)"));
        tbl.Append(CreateTableRow(false, w, "Domain registration (waterforall.org.za, 1 year)", "R 250"));
        tbl.Append(CreateTableRow(false, w, "Web hosting (shared, 1 year)", "R 2,400"));
        tbl.Append(CreateTableRow(false, w, "SSL Certificate (Let's Encrypt)", "Free"));
        tbl.Append(CreateTableRow(false, w, "Image assets and photography", "R 1,500"));
        tbl.Append(CreateTableRow(false, w, "Web development (design + coding)", "R 15,000"));
        tbl.Append(CreateTableRow(false, w, "Content writing and editing", "R 3,000"));
        tbl.Append(CreateTableRow(false, w, "Testing and quality assurance", "R 2,500"));
        tbl.Append(CreateTableRow(false, w, "Initial setup and deployment", "R 1,500"));
        tbl.Append(CreateTableRow(false, w, "Contingency (10%)", "R 2,615"));
        tbl.Append(CreateTableRow(true, w, "Total Initial Cost", "R 28,765"));
        tbl.Append(CreateTableRow(false, w, "Annual maintenance and hosting", "R 5,000"));
        return tbl;
    }

    private static TableProperties CreateTableProperties()
    {
        return new TableProperties(
            new TableWidth { Width = "5000", Type = TableWidthUnitValues.Pct },
            new TableBorders(
                new TopBorder { Val = BorderValues.Single, Size = 12, Color = Colors.Primary },
                new BottomBorder { Val = BorderValues.Single, Size = 12, Color = Colors.Primary },
                new LeftBorder { Val = BorderValues.Nil },
                new RightBorder { Val = BorderValues.Nil },
                new InsideHorizontalBorder { Val = BorderValues.Single, Size = 4, Color = Colors.Border },
                new InsideVerticalBorder { Val = BorderValues.Nil }));
    }

    private static TableRow CreateTableRow(bool isHeader, string[] widths, params string[] cells)
    {
        var row = new TableRow();
        if (isHeader) row.Append(new TableRowProperties(new TableHeader()));
        for (int i = 0; i < cells.Length; i++)
        {
            var cellWidth = i < widths.Length ? widths[i] : "2000";
            var tcp = new TableCellProperties(new TableCellWidth { Width = cellWidth, Type = TableWidthUnitValues.Dxa });
            if (isHeader) tcp.Append(new Shading { Val = ShadingPatternValues.Clear, Fill = Colors.TableHeader });
            var rpr = new RunProperties(new FontSize { Val = "20" }, new Color { Val = isHeader ? Colors.Deep : Colors.Mid });
            if (isHeader) rpr.Append(new Bold());
            row.Append(new TableCell(tcp, new Paragraph(
                new ParagraphProperties(new SpacingBetweenLines { Before = "40", After = "40" }),
                new Run(rpr, new Text(cells[i])))));
        }
        return row;
    }

    // ========================================================================
    // Image Helpers
    // ========================================================================
    private static string AddImage(MainDocumentPart mp, string path)
    {
        var ip = mp.AddImagePart(ImagePartType.Png);
        using var fs = new FileStream(path, FileMode.Open);
        ip.FeedData(fs); return mp.GetIdOfPart(ip);
    }

    private static Drawing CreateFloatingBackground(string imgId, uint prId, string name)
    {
        return new Drawing(new DW.Anchor(
            new DW.SimplePosition { X = 0, Y = 0 },
            new DW.HorizontalPosition(new DW.PositionOffset("0")) { RelativeFrom = DW.HorizontalRelativePositionValues.Page },
            new DW.VerticalPosition(new DW.PositionOffset("0")) { RelativeFrom = DW.VerticalRelativePositionValues.Page },
            new DW.Extent { Cx = A4WE, Cy = A4HE },
            new DW.EffectExtent { LeftEdge = 0, TopEdge = 0, RightEdge = 0, BottomEdge = 0 },
            new DW.WrapNone(),
            new DW.DocProperties { Id = prId, Name = name },
            new DW.NonVisualGraphicFrameDrawingProperties(new A.GraphicFrameLocks { NoChangeAspect = true }),
            new A.Graphic(new A.GraphicData(
                new PIC.Picture(
                    new PIC.NonVisualPictureProperties(
                        new PIC.NonVisualDrawingProperties { Id = 0, Name = $"{name}.png" },
                        new PIC.NonVisualPictureDrawingProperties()),
                    new PIC.BlipFill(new A.Blip { Embed = imgId }, new A.Stretch(new A.FillRectangle())),
                    new PIC.ShapeProperties(
                        new A.Transform2D(new A.Offset { X = 0, Y = 0 }, new A.Extents { Cx = A4WE, Cy = A4HE }),
                        new A.PresetGeometry { Preset = A.ShapeTypeValues.Rectangle })))
            { Uri = "http://schemas.openxmlformats.org/drawingml/2006/picture" }))
        { DistanceFromTop = 0, DistanceFromBottom = 0, DistanceFromLeft = 0, DistanceFromRight = 0,
          SimplePos = false, RelativeHeight = 251658240, BehindDoc = true,
          Locked = false, LayoutInCell = true, AllowOverlap = true });
    }

    // ========================================================================
    // Settings and Numbering
    // ========================================================================
    private static void SetUpdateFieldsOnOpen(MainDocumentPart mp)
    {
        var sp = mp.DocumentSettingsPart ?? mp.AddNewPart<DocumentSettingsPart>();
        sp.Settings = new Settings(new UpdateFieldsOnOpen { Val = true }, new DisplayBackgroundShape());
    }

    private static void AddNumbering(MainDocumentPart mp)
    {
        var np = mp.AddNewPart<NumberingDefinitionsPart>();
        np.Numbering = new Numbering(
            new AbstractNum(new Level(
                new NumberingFormat { Val = NumberFormatValues.Bullet },
                new LevelText { Val = "\u2022" },
                new LevelJustification { Val = LevelJustificationValues.Left },
                new ParagraphProperties(new Indentation { Left = "720", Hanging = "360" })
            ) { LevelIndex = 0 }) { AbstractNumberId = 1 },
            new NumberingInstance(new AbstractNumId { Val = 1 }) { NumberID = 1 });
    }
}

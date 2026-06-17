# Water For All

> **FICTITIOUS ORGANISATION**: Water For All is a fictitious organisation created solely for the WEDE5020 Web Development Portfolio of Evidence assignment. All content, statistics, team members, locations, and services are entirely fictional and exist for educational purposes only.

---

## WEDE5020 -- Portfolio of Evidence

**Student:Tovhakale Mulatedzi
**Student Number:ST10477234
**Subject:Web Development (WEDE5020)  
**Submission: Part 3 -- Enhancing Functionality and SEO  
**Date: 17 june 2026

---

## Table of Contents

1. [Project Overview](#project-overview)
2. [Part 2 Feedback Implementation](#part-2-feedback-implementation)
3. [JavaScript Enhancements](#javascript-enhancements)
4. [SEO Implementation](#seo-implementation)
5. [Form Validation](#form-validation)
6. [Website Structure](#website-structure)
7. [Technologies Used](#technologies-used)
8. [Changelog](#changelog)
9. [References](#references)

---

## Project Overview

Water For All is a fictitious non-profit organisation dedicated to providing sustainable access to clean, safe drinking water for underserved communities across South Africa. This project delivers a complete 5-page responsive website with embedded CSS and JavaScript, including form validation, interactive elements, and SEO optimisation.

### Website Goals

- Raise awareness about water scarcity in South Africa
- Provide information about water purification services and community outreach
- Facilitate enquiries about services, volunteering, and sponsorships
- Display contact information for multiple office locations
- Demonstrate modern web development techniques including HTML, CSS, and JavaScript

### Key Features

| Feature | Description |
|---------|-------------|
| Responsive Design | Fully responsive from 320px mobile to 1200px+ desktop |
| CSS-Only Mobile Menu | Checkbox hack hamburger menu (no JavaScript needed) |
| JavaScript Form Validation | Client-side validation on both enquiry and contact forms |
| CSS Accordion FAQ | Interactive FAQ using CSS checkbox technique |
| Animations | CSS keyframe animations (fadeInUp, hover transitions) |
| Scroll-to-Top Button | JavaScript-powered button that appears on scroll |
| SEO Optimised | Meta tags, sitemap.xml, robots.txt, semantic HTML |
| Fictitious Disclaimer | Clear banner on every page stating the organisation is fictional |

---

## Part 2 Feedback Implementation

Based on the Part 2 assessment feedback (88/100), the following corrections and improvements have been made for Part 3:

| Issue from Part 2 | Action Taken |
|---|---|
| Missing JavaScript functionality | Added comprehensive JavaScript form validation for both forms |
| No interactive elements | Added CSS accordion FAQ, hover animations, scroll-to-top button |
| SEO not fully implemented | Added meta descriptions, keywords, Open Graph tags, sitemap.xml, robots.txt |
| Organisation not clearly marked as fictitious | Added prominent orange fictitious banner on every page + footer disclaimer |
| Form validation missing | Implemented client-side JavaScript validation with error messages |
| No dynamic/interactive content | Added FAQ accordion, animated stat counters, interactive card hovers |

---

## JavaScript Enhancements

### 1. Form Validation (enquiry.html & contact.html)

Both forms include comprehensive client-side JavaScript validation:

- **Full Name**: Minimum 2 characters required
- **Email Address**: Regex pattern validation for proper email format
- **Phone Number**: Pattern validation allowing digits, spaces, dashes, and + prefix
- **Province (enquiry)**: Required dropdown selection
- **Subject (contact)**: Required dropdown selection
- **Message**: Minimum 10 characters required

Validation behaviour:
- Errors display in real-time when submitting
- Error styling (red border) appears on invalid fields
- Success message displays on valid submission
- Form resets after successful submission
- Error messages clear when user starts typing again

### 2. Scroll-to-Top Button

- Appears when user scrolls past 500px
- Smooth scroll animation to top of page
- Styled floating button with hover effect
- Implemented on all pages

### 3. Interactive Elements

- **CSS Accordion FAQ** (enquiry.html): Expandable/collapsible FAQ items using CSS checkbox hack
- **Card Hover Effects**: Service cards and feature cards lift on hover with shadow transitions
- **Navigation Hover**: Underline animation on nav links
- **Stat Animations**: Fade-in-up animation on homepage stats section

### 4. Mobile Navigation

- CSS-only hamburger menu using checkbox hack
- No JavaScript required for mobile menu toggle
- Smooth show/hide transition

---

## SEO Implementation

### On-Page SEO

Every page includes:
- **Title tags**: Unique, descriptive titles under 60 characters
- **Meta descriptions**: Unique descriptions under 160 characters
- **Meta keywords**: Relevant keywords for each page
- **Canonical URLs**: Proper canonical link tags
- **Semantic HTML5**: `<header>`, `<nav>`, `<main>`, `<section>`, `<article>`, `<footer>`
- **Heading hierarchy**: Proper H1 > H2 > H3 > H4 structure
- **Alt text**: Descriptive alt text on all images
- **Internal linking**: Cross-links between all pages

### SEO Files

| File | Purpose |
|------|---------|
| `sitemap.xml` | XML sitemap with all 5 pages, priorities, and change frequencies |
| `robots.txt` | Allows all crawlers, references sitemap.xml |

### Social Media Meta Tags

- Open Graph title and description tags on homepage
- Proper og:type set to "website"

---

## Form Validation

### Enquiry Form (enquiry.html)

Fields validated with JavaScript:
1. Enquiry Type (radio buttons) -- service, volunteer, or sponsorship
2. Full Name (text input) -- required, minimum 2 characters
3. Email Address (email input) -- required, valid email format
4. Phone Number (tel input) -- required, valid phone format
5. Organisation (text input) -- optional
6. Province (select dropdown) -- required
7. Message (textarea) -- required, minimum 10 characters

### Contact Form (contact.html)

Fields validated with JavaScript:
1. Full Name (text input) -- required, minimum 2 characters
2. Email Address (email input) -- required, valid email format
3. Subject (select dropdown) -- required, 7 options
4. Phone Number (tel input) -- optional
5. Message (textarea) -- required, minimum 10 characters

---

## Website Structure

```
water-for-all-part3/
|
|-- index.html              # Homepage (hero, stats, features, mission, CTA)
|-- about.html              # About Us (history, mission, values, team)
|-- services.html           # Services (core + additional programmes)
|-- enquiry.html            # Enquiry form (service/volunteer/sponsorship)
|-- contact.html            # Contact (3 offices, form, map, social)
|-- sitemap.xml             # XML sitemap for search engines
|-- robots.txt              # Robots directives for crawlers
|-- README.md               # This file -- project documentation
|-- images/
|   |-- hero-home.jpg
|   |-- about-water.jpg
|   |-- team.jpg
|   |-- service-purification.jpg
|   |-- service-conservation.jpg
|   |-- service-community.jpg
```

---

## Technologies Used

| Technology | Purpose |
|-----------|---------|
| HTML5 | Semantic page structure and content |
| CSS3 | Styling, responsive grid layout, animations, transitions |
| JavaScript (Vanilla) | Form validation, scroll-to-top button, interactive elements |

**No external frameworks or libraries** -- all code is hand-written vanilla HTML, CSS, and JavaScript for maximum learning and understanding.

---

## Changelog

### Part 3 Changes (from Part 2)

| Date | Change | Page(s) | Reason |
|------|--------|---------|--------|
| 2025-04-15 | Added fictitious organisation banner (orange) | All pages | Assignment requirement: organisation must be clearly marked as fictitious |
| 2025-04-15 | Updated footer with fictitious disclaimer | All pages | Assignment requirement: clear fictional disclaimer |
| 2025-04-15 | Added JavaScript form validation | enquiry.html, contact.html | Part 3 requirement: client-side form validation |
| 2025-04-15 | Added success/error message display | enquiry.html, contact.html | Improved user feedback for form submissions |
| 2025-04-15 | Added CSS accordion FAQ | enquiry.html | Part 3 requirement: interactive elements |
| 2025-04-15 | Added scroll-to-top button (JavaScript) | All pages | Enhanced user experience |
| 2025-04-15 | Added CSS animations (fadeInUp) | index.html | Part 3 requirement: animations and transitions |
| 2025-04-15 | Added card hover effects | All pages | Enhanced visual interactivity |
| 2025-04-15 | Added comprehensive SEO meta tags | All pages | Part 3 requirement: on-page SEO |
| 2025-04-15 | Created sitemap.xml | Root | Part 3 requirement: sitemap for SEO |
| 2025-04-15 | Created robots.txt | Root | Part 3 requirement: robots directives |
| 2025-04-15 | Improved mobile navigation | All pages | Better responsive experience |
| 2025-04-15 | Added Open Graph meta tags | index.html | Part 3 requirement: social media SEO |
| 2025-04-15 | Enhanced form styling with error states | enquiry.html, contact.html | Better visual feedback for validation |

### Part 2 (Previous Submission)

- Initial website creation with 5 HTML pages
- Embedded CSS styling with water-themed colour palette
- Semantic HTML5 structure
- Responsive design with mobile breakpoints
- Basic form structure (no validation)
- README.md with project documentation

---

## References

1. Department of Water and Sanitation (2024). National Water and Sanitation Master Plan. Available at: https://www.dws.gov.za (Accessed 15 April 2025).
2. Stats SA (2024). General Household Survey 2023. Pretoria: Statistics South Africa.
3. MDN Web Docs (2025). HTML: HyperText Markup Language. Available at: https://developer.mozilla.org/en-US/docs/Web/HTML (Accessed 12 April 2025).
4. MDN Web Docs (2025). CSS: Cascading Style Sheets. Available at: https://developer.mozilla.org/en-US/docs/Web/CSS (Accessed 12 April 2025).
5. MDN Web Docs (2025). JavaScript. Available at: https://developer.mozilla.org/en-US/docs/Web/JavaScript (Accessed 12 April 2025).
6. W3C (2024). HTML5 Specification. Available at: https://www.w3.org/TR/html5/ (Accessed 10 April 2025).
7. WCAG 2.1 (2018). Web Content Accessibility Guidelines 2.1. W3C Recommendation. Available at: https://www.w3.org/WAI/WCAG21/quickref/ (Accessed 14 April 2025).
8. Nielsen Norman Group (2024). 10 Usability Heuristics for User Interface Design. Available at: https://www.nngroup.com/articles/ten-usability-heuristics/ (Accessed 14 April 2025).
9. Mozilla (2025). Responsive Design Basics. Available at: https://developer.mozilla.org/en-US/docs/Learn/CSS/CSS_layout/Responsive_Design (Accessed 12 April 2025).
10. Google Search Central (2025). Search Engine Optimization (SEO) Starter Guide. Available at: https://developers.google.com/search/docs/fundamentals/seo-starter-guide (Accessed 14 April 2025).

---

## How to View

1. Download or clone this repository
2. Open `index.html` in any modern web browser
3. Navigate between pages using the menu

## GitHub Repository

[Link to your GitHub repository here]

## Deployed Website

[Link to your deployed website (Netlify/GitHub Pages) here]

---

*Water For All -- A fictitious organisation for the WEDE5020 Web Development assignment. All content is fictional and for educational purposes only.*

/**
 * Water For All - Main JavaScript File
 * Handles: mobile navigation toggle, form validation,
 * smooth scrolling, and interactive elements
 */

// ============================================================
// Wait for DOM to be fully loaded before attaching handlers
// ============================================================
document.addEventListener('DOMContentLoaded', function() {

    // ============================================================
    // 1. Mobile Navigation Toggle
    //    Shows/hides the navigation menu on small screens
    // ============================================================
    const menuToggle = document.querySelector('.menu-toggle');
    const mainNav = document.querySelector('.main-nav');

    if (menuToggle && mainNav) {
        menuToggle.addEventListener('click', function() {
            // Toggle the 'active' class to show/hide the mobile menu
            mainNav.classList.toggle('active');
            // Change the hamburger icon to X when menu is open
            const isOpen = mainNav.classList.contains('active');
            menuToggle.innerHTML = isOpen ? '&#10005;' : '&#9776;';
        });

        // Close mobile menu when a nav link is clicked
        const navLinks = mainNav.querySelectorAll('a');
        navLinks.forEach(function(link) {
            link.addEventListener('click', function() {
                mainNav.classList.remove('active');
                menuToggle.innerHTML = '&#9776;';
            });
        });
    }

    // ============================================================
    // 2. Form Validation
    //    Validates contact and enquiry forms before submission
    // ============================================================
    const contactForm = document.getElementById('contactForm');
    const enquiryForm = document.getElementById('enquiryForm');

    /**
     * validateField - checks if a single field has valid input
     * @param {HTMLElement} field - the input element to validate
     * @returns {boolean} - true if valid, false otherwise
     */
    function validateField(field) {
        const formGroup = field.closest('.form-group');
        let isValid = true;
        let errorMessage = '';

        // Remove existing error state
        formGroup.classList.remove('has-error');

        // Check if field is required and empty
        if (field.hasAttribute('required') && !field.value.trim()) {
            isValid = false;
            errorMessage = 'This field is required.';
        }
        // Email validation using regular expression pattern
        else if (field.type === 'email' && field.value) {
            const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
            if (!emailPattern.test(field.value)) {
                isValid = false;
                errorMessage = 'Please enter a valid email address.';
            }
        }
        // Phone validation - allow numbers, spaces, dashes, and + prefix
        else if (field.type === 'tel' && field.value) {
            const phonePattern = /^[\d\s\-+()]{7,20}$/;
            if (!phonePattern.test(field.value)) {
                isValid = false;
                errorMessage = 'Please enter a valid phone number.';
            }
        }
        // Minimum length validation
        else if (field.hasAttribute('minlength') && field.value.length < field.getAttribute('minlength')) {
            isValid = false;
            errorMessage = 'Input is too short.';
        }

        // Apply error state if invalid
        if (!isValid) {
            formGroup.classList.add('has-error');
            const errorEl = formGroup.querySelector('.form-error');
            if (errorEl) {
                errorEl.textContent = errorMessage;
            }
        }

        return isValid;
    }

    /**
     * setupFormValidation - attaches validation handlers to a form
     * @param {HTMLFormElement} form - the form to set up validation for
     */
    function setupFormValidation(form) {
        if (!form) return;

        const fields = form.querySelectorAll('input, textarea, select');

        // Validate on blur (when user leaves the field)
        fields.forEach(function(field) {
            field.addEventListener('blur', function() {
                validateField(field);
            });
            // Clear error when user starts typing again
            field.addEventListener('input', function() {
                const formGroup = field.closest('.form-group');
                formGroup.classList.remove('has-error');
            });
        });

        // Validate all fields on form submission
        form.addEventListener('submit', function(event) {
            event.preventDefault(); // Prevent actual form submission (demo)
            let isFormValid = true;

            fields.forEach(function(field) {
                if (!validateField(field)) {
                    isFormValid = false;
                }
            });

            if (isFormValid) {
                // Show success message
                const successMsg = form.querySelector('.form-success');
                if (successMsg) {
                    successMsg.style.display = 'block';
                    form.reset();
                    // Hide success message after 5 seconds
                    setTimeout(function() {
                        successMsg.style.display = 'none';
                    }, 5000);
                }
            }
        });
    }

    // Set up validation for both forms
    setupFormValidation(contactForm);
    setupFormValidation(enquiryForm);

    // ============================================================
    // 3. Smooth Scrolling for Anchor Links
    //    Smoothly scrolls to sections when clicking anchor links
    // ============================================================
    document.querySelectorAll('a[href^="#"]').forEach(function(anchor) {
        anchor.addEventListener('click', function(event) {
            const targetId = this.getAttribute('href');
            if (targetId === '#') return;

            const targetElement = document.querySelector(targetId);
            if (targetElement) {
                event.preventDefault();
                targetElement.scrollIntoView({
                    behavior: 'smooth',
                    block: 'start'
                });
            }
        });
    });

    // ============================================================
    // 4. Active Navigation Highlighting
    //    Highlights the current page in the navigation menu
    // ============================================================
    const currentPage = window.location.pathname.split('/').pop() || 'index.html';
    const navItems = document.querySelectorAll('.nav-list a');

    navItems.forEach(function(link) {
        const linkPage = link.getAttribute('href');
        if (linkPage === currentPage ||
            (currentPage === '' && linkPage === 'index.html') ||
            (currentPage === 'index.html' && linkPage === 'index.html')) {
            link.classList.add('active');
        }
    });

    // ============================================================
    // 5. Animated Counter for Stats (Homepage)
    //    Animates the statistics numbers counting up when visible
    // ============================================================
    const statNumbers = document.querySelectorAll('.stat-number[data-count]');

    if (statNumbers.length > 0) {
        /**
         * animateCounter - counts a number up from 0 to target
         * @param {HTMLElement} element - the element to animate
         * @param {number} target - the final number value
         * @param {number} duration - animation duration in ms
         */
        function animateCounter(element, target, duration) {
            let start = 0;
            const increment = target / (duration / 16); // 60fps
            const timer = setInterval(function() {
                start += increment;
                if (start >= target) {
                    element.textContent = target.toLocaleString();
                    clearInterval(timer);
                } else {
                    element.textContent = Math.floor(start).toLocaleString();
                }
            }, 16);
        }

        // Use IntersectionObserver to trigger animation when visible
        const observerOptions = {
            threshold: 0.5
        };

        const statsObserver = new IntersectionObserver(function(entries) {
            entries.forEach(function(entry) {
                if (entry.isIntersecting) {
                    const target = parseInt(entry.target.getAttribute('data-count'));
                    animateCounter(entry.target, target, 2000);
                    statsObserver.unobserve(entry.target);
                }
            });
        }, observerOptions);

        statNumbers.forEach(function(stat) {
            statsObserver.observe(stat);
        });
    }

    // ============================================================
    // 6. Scroll-to-Top Button
    //    Shows a button to scroll back to top after scrolling down
    // ============================================================
    // Create the button element dynamically
    const scrollTopBtn = document.createElement('button');
    scrollTopBtn.innerHTML = '&#9650;';
    scrollTopBtn.className = 'scroll-top-btn';
    scrollTopBtn.setAttribute('aria-label', 'Scroll to top');
    scrollTopBtn.style.cssText = '
        position: fixed;
        bottom: 30px;
        right: 30px;
        width: 50px;
        height: 50px;
        border-radius: 50%;
        background: linear-gradient(135deg, #0077B6, #00B4D8);
        color: #fff;
        border: none;
        font-size: 1.2rem;
        cursor: pointer;
        box-shadow: 0 4px 15px rgba(0,0,0,0.2);
        opacity: 0;
        visibility: hidden;
        transition: all 0.3s ease;
        z-index: 999;
    ';
    document.body.appendChild(scrollTopBtn);

    // Show/hide button based on scroll position
    window.addEventListener('scroll', function() {
        if (window.pageYOffset > 500) {
            scrollTopBtn.style.opacity = '1';
            scrollTopBtn.style.visibility = 'visible';
        } else {
            scrollTopBtn.style.opacity = '0';
            scrollTopBtn.style.visibility = 'hidden';
        }
    });

    // Scroll to top when clicked
    scrollTopBtn.addEventListener('click', function() {
        window.scrollTo({
            top: 0,
            behavior: 'smooth'
        });
    });

    // Hover effect for scroll button
    scrollTopBtn.addEventListener('mouseenter', function() {
        this.style.transform = 'translateY(-3px)';
        this.style.boxShadow = '0 6px 20px rgba(0,0,0,0.3)';
    });
    scrollTopBtn.addEventListener('mouseleave', function() {
        this.style.transform = 'translateY(0)';
        this.style.boxShadow = '0 4px 15px rgba(0,0,0,0.2)';
    });

}); // End DOMContentLoaded

/**
* Template Name: Kelly
* Template URL: https://bootstrapmade.com/kelly-free-bootstrap-cv-resume-html-template/
* Updated: Aug 07 2024 with Bootstrap v5.3.3
* Author: BootstrapMade.com
* License: https://bootstrapmade.com/license/
*/

(function () {
    "use strict";

    /**
     * Apply .scrolled class to the body as the page is scrolled down
     */
    function toggleScrolled() {
        const selectBody = document.querySelector('body');
        const selectHeader = document.querySelector('#header');
        if (!selectHeader.classList.contains('scroll-up-sticky') && !selectHeader.classList.contains('sticky-top') && !selectHeader.classList.contains('fixed-top')) return;
        if (window.scrollY > 100) {
            selectBody.classList.add('scrolled');
        } else {
            selectBody.classList.remove('scrolled');
        }
    }

    document.addEventListener('scroll', toggleScrolled);
    window.addEventListener('load', toggleScrolled);

    /**
     * Mobile nav toggle
     */
    const mobileNavToggleBtn = document.querySelector('.mobile-nav-toggle');

    function mobileNavToogle() {
        document.querySelector('body').classList.toggle('mobile-nav-active');
        mobileNavToggleBtn.classList.toggle('bi-list');
        mobileNavToggleBtn.classList.toggle('bi-x');
    }
    // Elementin sayfada var olup olmadýðýný kontrol edin
    if (mobileNavToggleBtn) {
        mobileNavToggleBtn.addEventListener('click', mobileNavToogle);
    }
    /**
     * Hide mobile nav on same-page/hash links
     */
    document.querySelectorAll('#navmenu a').forEach(navmenu => {
        navmenu.addEventListener('click', () => {
            if (document.querySelector('.mobile-nav-active')) {
                mobileNavToogle();
            }
        });
    });

    /**
     * Toggle mobile nav dropdowns
     */
    document.querySelectorAll('.navmenu .has-dropdown').forEach(navmenu => {
        navmenu.addEventListener('click', function (e) {
            if (document.querySelector('.mobile-nav-active')) {
                e.preventDefault();
                this.classList.toggle('active');
                this.nextElementSibling.classList.toggle('dropdown-active');
            }
        });
    });

    /**
     * Preloader
     */
    const preloader = document.querySelector('#preloader');
    if (preloader) {
        window.addEventListener('load', () => {
            preloader.remove();
        });
    }

    /**
     * Scroll top button
     */
    let scrollTop = document.querySelector('.scroll-top');

    function toggleScrollTop() {
        if (scrollTop) {
            window.scrollY > 100 ? scrollTop.classList.add('active') : scrollTop.classList.remove('active');
        }
    }
    window.addEventListener('load', toggleScrollTop);
    document.addEventListener('scroll', toggleScrollTop);

    /**
     * Animation on scroll function and init
     */
    function aosInit() {
        AOS.init({
            duration: 600,
            easing: 'ease-in-out',
            once: true,
            mirror: false
        });
    }
    window.addEventListener('load', aosInit);

})();

/* Token HttpOnly ayarlandýðý için JavaScript ile eriþilemez.

document.addEventListener('DOMContentLoaded', function () {
    const loginButton = document.getElementById('loginButtonContainer');
    const userIcon = document.getElementById('userIconContainer');

    console.log("Script çalýþýyor");

    // Belirli bir cookie adýný arayan güvenilir fonksiyon
    function getCookie(name) {
        const nameEQ = name + "=";
        const ca = document.cookie.split(';');
        for (let i = 0; i < ca.length; i++) {
            let c = ca[i];
            while (c.charAt(0) == ' ') c = c.substring(1, c.length);
            if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
        }
        return null;
    }

    // Token adýnýzýn "access_token" olduðunu varsayarak kontrol ediyoruz
    const token = getCookie('access_token');

    if (token) {
        console.log("Token bulundu");

        loginButton.style.display = 'none';
        userIcon.style.display = 'block';
    } else {
        console.log("Token bulunamadý");
        loginButton.style.display = 'block';
        userIcon.style.display = 'none';
    }
});
*/










document.addEventListener('DOMContentLoaded', function () {
    const userIcon = document.getElementById('userIconContainer');

    console.log("Script çalýþýyor");

    //Kullanýcý adýný alma
    var userName = document.cookie.split('; ').find(row => row.startsWith('user_name='));
  
    
    if (userName != null) {
        userName = userName.replace("user_name=", "");
        console.log("userName:", userName);

        console.log("userName bulundu");
        document.getElementById('userName').textContent = userName;
    } else {
        console.log("userName bulunamadý");
    }

    //Yetkiyi alma
    var role = document.cookie.split('; ').find(row => row.startsWith('role='));

    
    if (role != null) {

        role = role.replace("role=", "");
        console.log("role:", role);
        console.log("role bulundu");

        document.getElementById('role').textContent = role;
    } else {
        console.log("role bulunamadý");
    }
});

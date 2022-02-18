$(document).ready(function () {
    
    $(".banner-list").slick({
   
        dots: true,
        infinite: true,
        slidesToShow: 1,
        slidesToScroll: 1,
        arrows: false,
        autoplay: true,
        speed: 1500,
        autoplaySpeed: 5000,
    });
    $(".project-list").slick({
        slidesToShow: 1,
        slidesToScroll: 1,
        infinite: true,
        arrows: false,
        fade: false,
        cssEase: 'linear',
        asNavFor: '.nav-project',
    })
    $(".nav-project").slick({
        fade: false,
        cssEase: 'linear',
        infinite: true,
        centerMode: true,
        centerPadding: '400px',
        slidesToShow: 1,
        arrows: true,
        asNavFor: '.project-list',
        prevArrow: ('<div class="slick-prev"><i class="fas fa-caret-left"></i></div>'),
        nextArrow: ('<div class="slick-next"><i class="fas fa-caret-right"></i></div>'),
        responsive: [
            {
                breakpoint: 992,
                settings: {
                    // arrows: false,
                    centerMode: true,
                    centerPadding: '100px',
                    slidesToShow: 1
                }
            },
            {
                breakpoint: 480,
                settings: {
                    arrows: false,
                    centerMode: true,
                    centerPadding: '50px',
                    slidesToShow: 1
                }
            }
        ]
    });

    ///Team
    $(".personnel-list").slick({
        infinite: true,
        slidesToShow: 4,
        slidesToScroll: 1,
        arrows: false,
        autoplay: true,
        speed: 1500,
        autoplaySpeed: 3000,
        responsive: [
            {
                breakpoint: 769,
                settings: {
                    slidesToShow: 3,
                    slidesToScroll: 1,
                }
            },
            {
                breakpoint: 577,
                settings: {
                    slidesToShow: 1,
                }
            }
        ]
    });

    ///ProjectDetail
    $('.product-gallery').slick({
        slidesToShow: 1,
        slidesToScroll: 1,
        infinite: true,
        arrows: false,
        asNavFor: '.product-nav',

    });
    $('.product-nav').slick({
        slidesToShow: 5,
        slidesToScroll: 1,
        focusOnSelect: true,
        arrows: true,
        prevArrow: ('<div class="slick-prev"><i class="fas fa-chevron-left"></i></div>'),
        nextArrow: ('<div class="slick-next"><i class="fas fa-chevron-right"></i></div>'),
        asNavFor: '.product-gallery',
        responsive: [
            {
                breakpoint: 1024,
                settings: {
                    slidesToShow: 4,
                    slidesToScroll: 1,
                }
            },
            {
                breakpoint: 768,
                settings: {
                    slidesToShow: 3,
                    slidesToScroll: 1,
                }
            }
        ]
    });
    $('.chart').easyPieChart({
        size: 153,
    })
    ///menu-mobi
    $('.bars-mobi').click(function () {
        $('.bars-mobi i').toggleClass("fa-times");
        $('.header').toggleClass("show-menu");
    });
    $('.counter').countUp({
        delay: 5,
        time: 1000
    });
})

$(function () {
    AOS.init();

    $("#contact_form").on("submit", function (e) {
        e.preventDefault();
        if ($(this).valid()) {
            $.post("/Home/ContactForm", $(this).serialize(), function (data) {
                if (data.status) {
                    $.toast({
                        heading: 'Liên hệ thành công',
                        text: data.msg,
                        icon: 'success'
                    })
                    $("#contact_form").trigger("reset");
                } else {
                    $.toast({
                        heading: 'Liên hệ không thành công',
                        text: data.msg,
                        icon: 'error'
                    })
                }
            });
        }
    });


    $("#formSubscribe").on("submit", function (e) {
        e.preventDefault();
        $.post("/Home/QuickContact", $(this).serialize(), function (data) {
            if (data.status) {
                $.toast({
                    heading: 'Liên hệ thành công',
                    text: data.msg,
                    icon: 'success'
                })
                $("#formSubscribe").trigger("reset");
            } else {
                $.toast({
                    heading: 'Liên hệ không thành công',
                    text: data.msg,
                    icon: 'error'
                })
            }
        });
    });
})

///scoll-menu-index
window.onscroll = function () { scrollFunction() }
function scrollFunction() {
    if (document.body.scrollTop > 200 || document.documentElement.scrollTop > 200) {
        // document.getElementById("header").style.background = 'white';
        document.getElementById("header").classList.add("scoll-menu")
    } else {
        //   mybutton.style.display = "none";
        document.getElementById("header").classList.remove("scoll-menu")
        // document.getElementById("header").style.background = 'transparent';
    }

    
}
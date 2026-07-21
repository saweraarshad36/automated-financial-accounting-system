(function ($) {
    "use strict";

// TOP Menu Sticky
$(window).on('scroll', function () {
	var scroll = $(window).scrollTop();
	if (scroll < 100) {
    $("#sticky-header").removeClass("sticky");
    $('#back-top').fadeOut(500);
	} else {
    $("#sticky-header").addClass("sticky");
    $('#back-top').fadeIn(500);
	}
});
   


    $(document).ready(function () {

       
  $('[data-toggle="tooltip"]').tooltip();
// mobile_menu
var menu = $('ul#navigation');
if(menu.length){
	menu.slicknav({
		prependTo: ".mobile_menu",
		closedSymbol: '+',
		openedSymbol:'-'
	});
};

$('.luxary-carousel').owlCarousel({
  loop:false,
  margin:0,
items:1,
autoplay:true,
navText:['<img src="img/long-arrow-prev.png">', '<img src="img/long-arrow-next.png">'],
  nav:true,
dots:false,
autoplayHoverPause: true,
autoplaySpeed: 800,
  responsive:{
      0:{
          items:1,
          nav:false,
      },
      767:{
          items:1,
          nav:false,
      },
      992:{
          items:1,
          nav:false
      },
      1200:{
          items:1,
          nav:false
      },
      1600:{
          items:1,
          nav:true
      }
  }
});
$('.main-slider').owlCarousel({
  center: false,
  items: 1,
  loop: true,
stagePadding: 0,
  margin: 0,
  smartSpeed: 1500,
  autoplay: true,
  pauseOnHover: false,
  dots: false,
  nav: true,
  navText: ['<i class="fa fa-chevron-left" aria-hidden="true"></i>', '<i class="fa fa-chevron-right" aria-hidden="true"></i>']
});
// property image thumbs slider
    $('.property_image_Carousel').owlCarousel({        
  center: false,
  items: 1,
  loop: false,
stagePadding: 0,
  margin: 0,
  autoplay: false,
  dots: true,
  nav: true,
  navText: ['<i class="fa fa-chevron-left" aria-hidden="true"></i>', '<i class="fa fa-chevron-right" aria-hidden="true"></i>']
});

// review-active


// review-active
$('.property_banner_active').owlCarousel({
  loop:true,
  margin:30,
items:1,
autoplay:false,
navText:['<i class="ti-angle-left"></i>','<i class="ti-angle-right"></i>'],
  nav:true,
dots:true,
autoplayHoverPause: true,
autoplaySpeed: 800,
// dotsData: true,
center: false,
  responsive:{
      0:{
          items:1,
          nav:false
      },
      767:{
          items:1,
          nav:false
      },
      992:{
          items:1
      },
      1200:{
          items:1
      },
      1500:{
          items:1,
          nav:true
      }
  }
});

  // scrollIt for smoth scroll
  $.scrollIt({
    upKey: 38,             // key code to navigate to the next section
    downKey: 40,           // key code to navigate to the previous section
    easing: 'linear',      // the easing function for animation
    scrollTime: 600,       // how long (in ms) the animation takes
    activeClass: 'active', // class given to the active nav element
    onPageChange: null,    // function(pageIndex) that is called when page is changed
    topOffset: 0           // offste (in px) for fixed top navigation
  });

  // scrollup bottom to top
  $.scrollUp({
    scrollName: 'scrollUp', // Element ID
    topDistance: '4500', // Distance from top before showing element (px)
    topSpeed: 300, // Speed back to top (ms)
    animation: 'fade', // Fade, slide, none
    animationInSpeed: 200, // Animation in speed (ms)
    animationOutSpeed: 200, // Animation out speed (ms)
    scrollText: '<i class="fa fa-angle-double-up"></i>', // Text for element
    activeOverlay: false, // Set CSS color to display scrollUp active point, e.g '#00FFFF'
  });


  // blog-page


if (document.getElementById('default-select')) {
  $('select').niceSelect();
}

  //about-pro-active
$('.details_active').owlCarousel({
  loop:true,
  margin:0,
items:1,
// autoplay:true,
navText:['<i class="ti-angle-left"></i>','<i class="ti-angle-right"></i>'],
nav:true,
dots:false,
// autoplayHoverPause: true,
// autoplaySpeed: 800,
  responsive:{
      0:{
          items:1,
          nav:false

      },
      767:{
          items:1,
          nav:false
      },
      992:{
          items:1,
          nav:false
      },
      1200:{
          items:1,
      }
  }
});

});



    // Search Toggle
    $("#search_input_box").hide();
    $("#search").on("click", function () {
        $("#search_input_box").slideToggle();
        $("#search_input").focus();
    });
    $("#close_search").on("click", function () {
        $('#search_input_box').slideUp(500);
    });
    // Search Toggle
    $("#search_input_box").hide();
    $("#search_1").on("click", function () {
        $("#search_input_box").slideToggle();
        $("#search_input").focus();
    });
    $(document).ready(function() {
      $('select').niceSelect();
    });

    // prise slider 
     
    
    $('header').css('height', $('.header-area').outerHeight());
// Magnific popup initializing
$('.popup-gallery').magnificPopup({
  delegate: 'a',
  type: 'image',
  tLoading: 'Loading image #%curr%...',
  mainClass: 'mfp-img-mobile',
  gallery: {
    enabled: true,
    navigateByImgClick: true,
    preload: [0,1] // Will preload 0 - before current, and 1 after the current image
  },
  image: {
    tError: '<a href="%url%">The image #%curr%</a> could not be loaded.',

  }
});

    $('.chat-icon').click(function () {
        $('.chat-popup').toggleClass('openchat');
        if ($('.chat-popup').hasClass('openchat')) {
            $('.chat-icon').html('<i class="fa fa-times text-white"></i>')
        }
        else {
            $('.chat-icon').html('<i class="fa fa-comment text-white"></i>')
        }
    });
    
})(jQuery);	

function owlCarouselSetTime() {
    $('.popup-gallery').owlCarousel({
        loop: false,
        nav: true,
        dots: false,
        navText: ["<i class='fa fa-angle-left'></i>", "<i class='fa fa-angle-right'></i>"],
        responsive: {
            0: {
                items: 1
            },
            600: {
                items: 1
            },
            1000: {
                items: 1
            }
        }
    })
    shownavbar();
}

function shownavbar() {
    var propertylength = $('.property_list_item_inner').length;
    for (var i = 0; i <= propertylength; i++) {
        if ($('.property_list_item_inner:nth-child(' + i + ') .item').length < 2) {
            $('.property_list_item_inner:nth-child(' + i + ')').find('.owl-nav').addClass('d-none');
            $('.property_list_item_inner:nth-child(' + i + ')').find('.owl-dots').addClass('d-none');
        }
    }
}


function owlCarouselSync() {
    var sync1 = $(".sliderContent");
    var sync2 = $(".navigation-thumbs");

    var thumbnailItemClass = '.owl-item';

    var slides = sync1.owlCarousel({
        items: 1,
        
        loop: true,
        nav: true,
        navText: ['<i class="fa fa-chevron-left" aria-hidden="true"></i>', '<i class="fa fa-chevron-right" aria-hidden="true"></i>'],
        dots: false
    }).on('changed.owl.carousel', syncPosition);

    function syncPosition(el) {
        $owl_slider = $(this).data('owl.carousel');
        var loop = $owl_slider.options.loop;

        if (loop) {
            var count = el.item.count - 1;
            var current = Math.round(el.item.index - (el.item.count / 2) - .5);
            if (current < 0) {
                current = count;
            }
            if (current > count) {
                current = 0;
            }
        } else {
            var current = el.item.index;
        }

        var owl_thumbnail = sync2.data('owl.carousel');
        var itemClass = "." + owl_thumbnail.options.itemClass;


        var thumbnailCurrentItem = sync2
            .find(itemClass)
            .removeClass("synced")
            .eq(current);

        thumbnailCurrentItem.addClass('synced');

        if (!thumbnailCurrentItem.hasClass('active')) {
            var duration = 300;
            sync2.trigger('to.owl.carousel', [current, duration, true]);
        }
    }
    var thumbs = sync2.owlCarousel({
        startPosition: 12,
        items: 4,
        loop: false,
        
        autoplay: false,
        nav: true,
        navText: ['<i class="fa fa-chevron-left" aria-hidden="true"></i>', '<i class="fa fa-chevron-right" aria-hidden="true"></i>'],
        dots: false,
        onInitialized: function (e) {
            var thumbnailCurrentItem = $(e.target).find(thumbnailItemClass).eq(this._current);
            thumbnailCurrentItem.addClass('synced');
        },
    })
        .on('click', thumbnailItemClass, function (e) {
            e.preventDefault();
            var duration = 300;
            var itemIndex = $(e.target).parents(thumbnailItemClass).index();
            sync1.trigger('to.owl.carousel', [itemIndex, duration, true]);
        }).on("changed.owl.carousel", function (el) {
            var number = el.item.index;
            $owl_slider = sync1.data('owl.carousel');
            $owl_slider.to(number, 100, true);
        });

}
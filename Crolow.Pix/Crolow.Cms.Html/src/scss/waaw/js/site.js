import $ from "jquery";

// Open overlay
document.querySelectorAll('.sidebar-icon').forEach(btn => {
  btn.addEventListener('click', () => {
    const target = document.querySelector(btn.dataset.target);
    document.querySelectorAll('.sidebar-overlay').forEach(o => o.classList.remove('active'));
    target.classList.add('active');
  });
});

document.querySelectorAll('.menu-item a').forEach(btn => {
  btn.addEventListener('click', e => {
    const targetSelector = btn.dataset.target;
    if (!targetSelector) {
      return;
    }

    // dataset.target is set → prevent anchor navigation
    e.preventDefault();

    const target = document.querySelector(targetSelector);
    if (!target) return; // guard if selector not found

    document.querySelectorAll('.sidebar-overlay')
      .forEach(o => o.classList.remove('active'));

    target.classList.add('active');
  });
});

// Back buttons
document.querySelectorAll('.sidebar-overlay .back-btn').forEach(btn => {
  btn.addEventListener('click', () => {
    const overlay = btn.closest('.sidebar-overlay');
    const submenu = btn.closest('.overlay-submenu');
    if (submenu) {
      submenu.classList.remove('active');
      // overlay.querySelector('.overlay-menu').style.display = '';
    } else {
      overlay.classList.remove('active');
    }
  });
});

// Open submenus
document.querySelectorAll('.menu-item').forEach(btn => {
  btn.addEventListener('click', () => {
    const target = document.querySelector(btn.dataset.target);
    const overlay = btn.closest('.sidebar-overlay');
    //overlay.querySelector('.overlay-menu').style.display = 'none';
    target.classList.add('active');
  });
});


document.addEventListener("DOMContentLoaded", () => {
  const punchlines = document.querySelectorAll(".punchline");
  let currentIndex = -1;

  function showRandomPunchline() {
    // Hide current punchline
    if (currentIndex >= 0) {
      punchlines[currentIndex].classList.remove("show");
    }

    // Pick a new index different from the last one
    let newIndex;
    do {
      newIndex = Math.floor(Math.random() * punchlines.length);
    } while (newIndex === currentIndex);

    currentIndex = newIndex;
    punchlines[currentIndex].classList.add("show");
  }

  // Initial display
  showRandomPunchline();

  // Change every 4 seconds
  setInterval(showRandomPunchline, 4000);
});


//$(document).ready(function () {
//  $('.slicker').not('.slick-initialized').slick({
//    dots: true,
//    infinite: true,
//    centerMode: true,
//    slidesToShow: 4,
//    slidesToScroll: 1,
//    responsive: [
//      {
//        breakpoint: 768,
//        settings: {
//          arrows: true,
//          centerMode: true,
//          centerPadding: '40px',
//          slidesToShow: 1
//        }
//      },
//      {
//        breakpoint: 480,
//        settings: {
//          arrows: false,
//          centerMode: true,
//          centerPadding: '40px',
//          slidesToShow: 1
//        }
//      }
//    ]
//  });
//});

//$(function () {

//  function equalizeAll() {
//    $(".slicker").each(function () {
//      equalize($(this));
//    });
//  }

//  function equalize($slider) {
//    // work only with original slides (skip clones)
//    if ($slider === undefined) {
//      console.log("Equalizing heights for", $slider);
//      return;
//    }
//    const $cards = $slider.find(".slick-slide:not(.slick-cloned) .card, > .card");
//    if (!$cards.length) return;

//    let max = 0;
//    $cards.each(function () {
//      const h = $(this).outerHeight(); // include padding/border
//      if (h > max) max = h;
//    });

//    // inject CSS rule with !important, but scoped to this slider
//    const styleId = "equalize-heights-style-" + $slider.index();
//    let styleTag = document.getElementById(styleId);
//    if (!styleTag) {
//      styleTag = document.createElement("style");
//      styleTag.id = styleId;
//      document.head.appendChild(styleTag);
//    }
//    // scope to this slicker only
//    styleTag.textContent = `#${$slider.attr("id")} .card { height: ${max}px !important; }`;
//  }

//  // resize/orientation changes
//  let resizeTimer;
//  $(window).on("resize orientationchange", function () {
//    clearTimeout(resizeTimer);
//    resizeTimer = setTimeout(equalizeAll, 100);
//  });

//  $(".slicker").each(function () {
//    const $slider = $(this);

//    // read slides config
//    const slides = $slider.data("slides");
//    const slideArray = (slides && slides.trim())
//      ? slides.split(',').map(x => parseInt(x.trim(), 10))
//      : [4, 3, 2, 1];

//    // initialize slick
//    $slider.slick({
//      slidesToShow: slideArray[0],
//      slidesToScroll: 1,
//      infinite: true,
//      adaptiveHeight: false,
//      responsive: [
//        { breakpoint: 992, settings: { slidesToShow: slideArray[1] } },
//        { breakpoint: 768, settings: { slidesToShow: slideArray[2] } },
//        { breakpoint: 576, settings: { slidesToShow: slideArray[3] } }
//      ]
//    });

//    $slider.on("init reInit setPosition afterChange", function () { equalize($slider); });
//    setTimeout(equalize, 0);

//    // re-equalize when images load
//    $slider.find("img").each(function () {
//      if (!this.complete) {
//        $(this).one("load error", function () { equalize($slider); });
//      }
//    });
//  });
//});

$(function () {

  // Equalize heights for a single slider
  function equalize($slider) {
    if (!$slider || !$slider.length) return;

    // select only original slides (skip clones)
    const $cards = $slider.find(".slick-slide:not(.slick-cloned) .card, > .card");
    if (!$cards.length) return;

    // reset heights first
    //$cards.css("height", "auto");
    $cards.parent().css("height", "auto");

    // find max height
    let max = 0;
    $cards.each(function () {
      const h = $(this).outerHeight();
      if (h > max) max = h;
    });

    // apply max height to all cards
    //$cards.css("height", max + "px");
    $cards.parent().css("height", max + "px");
  }

  // Equalize all sliders on the page
  function equalizeAll() {
    $(".slicker").each(function () {
      equalize($(this));
    });
  }

  // Debounced resize handler
  let resizeTimer;
  $(window).on("resize orientationchange", function () {
    clearTimeout(resizeTimer);
    resizeTimer = setTimeout(equalizeAll, 100);
  });

  // Initialize each slider
  $(".slicker").each(function () {
    const $slider = $(this);

    // Read slides config from data attribute
    const slides = $slider.data("slides");
    const slideArray = (slides && slides.trim())
      ? slides.split(',').map(x => parseInt(x.trim(), 10))
      : [4, 3, 2, 1];

    // Initialize slick
    $slider.slick({
      slidesToShow: slideArray[0],
      slidesToScroll: 1,
      infinite: true,
      adaptiveHeight: false,
      responsive: [
        { breakpoint: 992, settings: { slidesToShow: slideArray[1] } },
        { breakpoint: 768, settings: { slidesToShow: slideArray[2] } },
        { breakpoint: 576, settings: { slidesToShow: slideArray[3] } }
      ]
    });

    // Slick events
    $slider.on("init reInit setPosition afterChange", function () {
      equalize($slider);
    });

    // Equalize once after initialization
    setTimeout(function () { equalize($slider); }, 0);

    // Re-equalize when images finish loading
    $slider.find("img").each(function () {
      if (!this.complete) {
        $(this).one("load error", function () { equalize($slider); });
      }
    });
  });

});

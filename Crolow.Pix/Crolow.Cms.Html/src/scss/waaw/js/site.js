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

$(function () {
  const $slider = $(".slicker");

  function equalize() {
    // work only with original slides (skip clones)
    const $cards = $slider.find(".slick-slide:not(.slick-cloned) .card, > .card");
    if (!$cards.length) return;

    //$cards.css("height", "auto"); // reset
    let max = 0;
    $cards.each(function () {
      const h = $(this).outerHeight(); // include padding/border
      if (h > max) max = h;
    });

    // apply to originals AND clones to keep everything aligned
    //$cards.css("height", max + "px !important");

    // Inject CSS rule with !important
    const styleId = "equalize-heights-style";
    let styleTag = document.getElementById(styleId);
    if (!styleTag) {
      styleTag = document.createElement("style");
      styleTag.id = styleId;
      document.head.appendChild(styleTag);
    }
    styleTag.textContent = `.slicker .card { height: ${max}px !important; }`;
  }

  // Initialize slick
  $slider.slick({
    slidesToShow: 4,
    slidesToScroll: 1,
    infinite: true,
    adaptiveHeight: false,   // don't let Slick change track height per slide
    responsive: [
      { breakpoint: 992, settings: { slidesToShow: 3 } },
      { breakpoint: 768, settings: { slidesToShow: 2 } },
      { breakpoint: 576, settings: { slidesToShow: 1 } }
    ]
  });

  // Run equalizer at the right times
  $slider.on("init reInit setPosition afterChange", equalize);
  // If Slick fired 'init' before handlers attached, force a pass:
  setTimeout(equalize, 0);

  // Re-equalize when images finish loading (important!)
  $slider.find("img").each(function () {
    if (this.complete) return;
    $(this).one("load error", equalize);
  });

  // Resize/orientation changes
  $(window).on("resize orientationchange", equalize);
});

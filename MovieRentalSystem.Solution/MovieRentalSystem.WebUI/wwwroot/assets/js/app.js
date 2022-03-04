$(document).ready(function () {
  $("header").load("_header.html");
  $("footer").load("_footer.html");

  let filmsSwiperCarousel = new Swiper(".films-swiper-carousel", {
    pagination: {
      el: ".swiper-pagination",
      dynamicBullets: true,
    },
  });

  let filmsSwiperItem = $(".films-swiper-item");

  $.each(filmsSwiperItem, (index, item) => {
    let bgThumbFilmsSwiper = $(item).attr("bg-thumb");

    $(item).css(
      "background-image",
      `linear-gradient(to top, #1e2129, transparent), url(${bgThumbFilmsSwiper})`
    );
  });

  $(".latest-movies-carousel").owlCarousel({
    margin: 10,
    responsive: {
      0: {
        items: 1,
      },
      600: {
        items: 3,
      },
      1000: {
        items: 5,
      },
    },
  });

  $(".new-trailer-play-btn").magnificPopup({
    type: "iframe",
  });

  let recentMoviesBtns = $(".recent-movies-right-side>button");

  $(recentMoviesBtns).on("click", function () {
    $(recentMoviesBtns).removeClass("active");
    $(this).addClass("active");
  });

  let selectedMovieSection = $("#selected-movie");
  let bgThumbSelectedMovie = $(selectedMovieSection).attr("bg-thumb");
  $(selectedMovieSection).css(
    "background-image",
    `url(${bgThumbSelectedMovie})`
  );

  $(".selected-movie-play-btn").magnificPopup({
    type: "iframe",
  });

  let mixer = mixitup(".recent-movies-row", {
    selectors: {
      target: ".recent-movies-cards-col",
    },
    animation: {
      duration: 300,
    },
  });
});

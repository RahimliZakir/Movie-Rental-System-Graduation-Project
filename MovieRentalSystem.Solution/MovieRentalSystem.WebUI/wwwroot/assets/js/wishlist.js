$(document).ready(function () {
  $("header").load("_header.html");
  $("footer").load("_footer.html");

  let wishlistFilterMoviesBtn = $(".wishlist-filter-movies-btn");
  let wishlistFilterShowsBtn = $(".wishlist-filter-shows-btn");
  let wishlistMoviesRow = $(".wishlist-movies-row");
  let wishlistShowsRow = $(".wishlist-shows-row");

  if ($(wishlistFilterMoviesBtn).hasClass("selected")) {
    $(wishlistShowsRow).removeClass("visible");
    $(wishlistMoviesRow).addClass("visible");
  } else {
    $(wishlistMoviesRow).removeClass("visible");
    $(wishlistShowsRow).addClass("visible");
  }

  let wishlistMoviesCards = $(wishlistMoviesRow).children().find(".card");
  let wishlistShowsCards = $(wishlistShowsRow).children().find(".card");

  let wishlistMoviesImgDivs = $(wishlistMoviesCards).find(".img-div");
  let wishlistShowsImgDivs = $(wishlistShowsCards).find(".img-div");
  $.each(wishlistMoviesImgDivs, function (index, item) {
    let timesIcon = $("<i/>", {
      class: "bi bi-x-circle remove-movie-btn",
      style:
        "cursor: pointer;position: absolute;right: 5px;bottom: 5px;color: #fff;z-index: 2;",
    });
    $(item).prepend(timesIcon);
  });
  $.each(wishlistShowsImgDivs, function (index, item) {
    let timesIcon = $("<i/>", {
      class: "bi bi-x-circle remove-show-btn",
      style:
        "cursor: pointer;position: absolute;right: 5px;bottom: 5px;color: #fff;z-index: 2;",
    });
    $(item).prepend(timesIcon);
  });

  let moviesTimesIcons = $(wishlistMoviesCards).find(".remove-movie-btn");
  $(moviesTimesIcons).click(function () {
    $(this).closest(".latest-movies-card-col").remove();
  });
  let showsTimesIcons = $(wishlistShowsCards).find(".remove-show-btn");
  $(showsTimesIcons).click(function () {
    $(this).closest(".latest-tv-show-bottom-col").remove();
  });

  $(wishlistFilterMoviesBtn).on("click", (e) => {
    $(wishlistShowsRow).removeClass("visible");
    $(wishlistMoviesRow).addClass("visible");

    $(e.currentTarget).siblings().removeClass("selected");
    $(e.currentTarget).addClass("selected");
  });

  $(wishlistFilterShowsBtn).on("click", (e) => {
    $(wishlistMoviesRow).removeClass("visible");
    $(wishlistShowsRow).addClass("visible");

    $(e.currentTarget).siblings().removeClass("selected");
    $(e.currentTarget).addClass("selected");
  });
});

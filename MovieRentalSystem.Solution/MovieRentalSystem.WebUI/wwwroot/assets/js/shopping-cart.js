$(function () {
  $("header").load("_header.html");
  $("footer").load("_footer.html");

  $(".select-for-days-select").select2();

  let shoppingCartFilterMovies = $(".shopping-cart-filter-movies-btn");
  let shoppingCartFilterShows = $(".shopping-cart-filter-shows-btn");

  let shoppinCartMoviesRow = $(".movie-checkout-box-row");
  let shoppinCartShowsRow = $(".shows-checkout-box-row");

  $(shoppingCartFilterMovies).on("click", (e) => {
    $(shoppinCartShowsRow).removeClass("visible");
    $(shoppinCartMoviesRow).addClass("visible");

    $(e.currentTarget).siblings().removeClass("selected");
    $(e.currentTarget).addClass("selected");
  });

  $(shoppingCartFilterShows).on("click", (e) => {
    $(shoppinCartMoviesRow).removeClass("visible");
    $(shoppinCartShowsRow).addClass("visible");

    $(e.currentTarget).siblings().removeClass("selected");
    $(e.currentTarget).addClass("selected");
  });
});

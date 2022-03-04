$(document).ready(function () {
  $("header").load("_header.html");
  $("footer").load("_footer.html");

  let stars = $(".star-rating-div>i");

  $(stars).on("click", function () {
    $(stars).removeClass("fas").addClass("far");
    $(this).removeClass("far").addClass("fas");
    $(this).prevAll("i").removeClass("far").addClass("fas");
  });

  $(".cinema-seats .seat").on("click", function () {
    $(this).toggleClass("active");
  });
});

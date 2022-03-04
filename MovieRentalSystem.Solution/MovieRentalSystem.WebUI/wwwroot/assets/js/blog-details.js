$(function () {
  $("header").load("_header.html");
  $("footer").load("_footer.html");

  const blogDescriptionSwiper = new Swiper(".blog-description-swiper", {
    speed: 400,
    spaceBetween: 100,
    pagination: {
      el: ".swiper-pagination",
      type: "bullets",
    },
  });
});

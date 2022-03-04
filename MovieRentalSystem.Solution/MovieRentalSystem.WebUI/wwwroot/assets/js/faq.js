$(function () {
  $("header").load("_header.html");
  $("footer").load("_footer.html");

  let accHeading = $(".acc-heading");

  $(accHeading).click(function () {
    $(this)
      .closest(".faq-card")
      .siblings()
      .find(".faq-accordion-body")
      .slideUp();
    $(this)
      .closest(".faq-card")
      .siblings()
      .find(".acc-heading")
      .find("i")
      .removeClass("bi-dash-lg")
      .addClass("bi-plus-lg");

    if ($(this).parent().next().css("display") == "none") {
      $(this).find("i").removeClass("bi-plus-lg").addClass("bi-dash-lg");
      $(this).parent().next().slideDown();
    } else {
      $(this).find("i").removeClass("bi-dash-lg").addClass("bi-plus-lg");
      $(this).parent().next().slideUp();
    }
  });
});

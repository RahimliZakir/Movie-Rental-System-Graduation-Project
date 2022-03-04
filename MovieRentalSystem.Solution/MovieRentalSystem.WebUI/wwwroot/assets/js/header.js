window.addEventListener("load", function () {
  let currentLocation = window.location.href;
  let menus = document.querySelectorAll(".nav-links>li");
  let collapsedMenus = document.querySelectorAll(".collapsed-nav-links>li");
  let menusLength = menus.length;

  for (let i = 0; i < menusLength; i++) {
    if (
      menus[i].children[0].href == currentLocation &&
      collapsedMenus[i].children[0].href
    ) {
      menus[i].children[0].classList.add("active");
      collapsedMenus[i].children[0].classList.add("active");
    }
  }

  $(".right-nav-collapse-btn").on("click", () => {
    $(".collapsed-nav-items").slideToggle();
  });
});

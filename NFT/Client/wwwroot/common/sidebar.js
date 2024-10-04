var sidebar = document.getElementsByClassName("sidebar")[0];
var burgerBtn = document.getElementById("burger-btn");

burgerBtn.addEventListener("click", function () {
  sidebar.style.right = "0";
});

document.addEventListener("click", function (e) {
  if (e.target !== sidebar && e.target !== burgerBtn) {
    sidebar.style.right = "-300px";
  }
});

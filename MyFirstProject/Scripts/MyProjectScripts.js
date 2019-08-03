function openNav() {
    document.getElementById("mySidenav").style.width = "250px";
}
function closeNav() {
    document.getElementById("mySidenav").style.width = "0";
}
$(document).ready(function () {
    $(document).click(function (event) {
        var clickover = $(event.target);
        var _opened = $(".sidenav").hasClass("nav-side");
        if (_opened === true && !clickover.hasClass("nav-side")) {
            document.getElementById("mySidenav").style.width = "0";
        }
    });
});
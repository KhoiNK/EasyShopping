$(document).ready(function () {
    //Handles menu drop down
    $('.dropdown-menu').find('form').click(function (e) {
        e.stopPropagation();
    });
});

window.onclick = function (e) {
    if (!e.target.matches('#dropdown')) {
        var myDropdown = document.getElementById("mydropdown");
        
        if (myDropdown.classList.contains('in')) {
            myDropdown.classList.remove('in');
            myDropdown.classList.add('collapse');
            //myDropdown.hidden = true;
        }
    }
}
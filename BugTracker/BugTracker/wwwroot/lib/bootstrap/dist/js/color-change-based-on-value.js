

// Changes badge colors in table in /Tickets tab
function statusColor() {
    var el = document.querySelectorAll(".color_change_status");

    for (var i = 0; i < el.length; i++) {

        var text = el[i].textContent.trim().replace(/&nbsp;/g, '');

        if (text == "Open")
            el[i].setAttribute("class", "badge bg-success");
        else if (text == "In progress")
            el[i].setAttribute("class", "badge bg-warning");
        else if (text == "To do")
            el[i].setAttribute("class", "badge bg-info");
        else if (text == "To be tested")
            el[i].setAttribute("class", "badge bg-danger");
        else if (text == "Closed")
            el[i].setAttribute("class", "badge bg-secondary");
        else
            el[i].setAttribute("class", "badge bg-light");
    }

    var el_2 = document.querySelectorAll(".color_change_severity");

    for (var i = 0; i < el_2.length; i++) {

        var text = el_2[i].textContent.trim().replace(/&nbsp;/g, '');


        if (text == "Low")
            el_2[i].setAttribute("class", "badge bg-success");
        else if (text == "Medium")
            el_2[i].setAttribute("class", "badge bg-warning");
        else if (text == "High")
            el_2[i].setAttribute("class", "badge bg-danger");
        else
            el_2[i].setAttribute("class", "badge bg-light");
    }
}


// Removes seconds (useless) from 'Deadline' column in /Tickets tab
function removeTime() {
    var el_3 = document.querySelectorAll(".remove_time");

    for (var j = 0; j < el_3.length; j++) {

        var date = el_3[j].textContent.trim().replace(/&nbsp;/g, '').slice(0, -8);
        el_3[j].innerHTML = date;
    }
}

function removeSeconds() {
    var el_4 = document.querySelectorAll(".remove_seconds");

    for (var j = 0; j < el_4.length; j++) {

        var date = el_4[j].textContent.trim().replace(/&nbsp;/g, '').slice(0, -3);
        el_4[j].innerHTML = date;
    }
}
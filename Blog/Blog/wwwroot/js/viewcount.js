// wwwroot/js/viewcount.js
function animateCounter(id, endValue) {
    let element = document.getElementById(id);
    if (!element) return;

    let start = 0;
    let duration = 1500;
    let increment = Math.ceil(endValue / (duration / 50));

    let counter = setInterval(() => {
        start += increment;
        if (start >= endValue) {
            start = endValue;
            clearInterval(counter);
        }
        element.innerText = start;
    }, 50);
}

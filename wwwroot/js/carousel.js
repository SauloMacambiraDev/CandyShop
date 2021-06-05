var currentCarouselElement = 1;
var carouselGoingToRight = true;
var carouselEl = document.querySelector("#carousel-items")

carouselEl.addEventListener("wheel", function (event) {
    console.log(event.target);
    if (event.deltaY < 0) {
        moveCarouselToRight(event.target);
    } else {
        moveCarouselToLeft(event.target);
    }
});

document.querySelector("#move-carousel-left").addEventListener("click", function (event) {
    moveCarouselToLeft();
});

document.querySelector("#move-carousel-right").addEventListener("click", function (event) {
    moveCarouselToRight();
});

function moveCarouselToRight() {
    console.log("scrolling UP!");
    carouselEl.scrollBy(600, 0);
}

function moveCarouselToLeft() {
    console.log("Scrolling DOWN!");
    carouselEl.scrollBy(-600, 0);
}

document.onreadystatechange = function () {
    if (document.readyState === 'complete') {
        setInterval(function () {
            // CHECK CAROUSEL DIRECTION
            if (currentCarouselElement === 1) {
                carouselGoingToRight = true;
            }
            if (currentCarouselElement === 3) {
                carouselGoingToRight = false;
            }

             // MOVING ACCORDING TO CAROUSEL DIRECTION
            if (carouselGoingToRight) {
                moveCarouselToRight()
                currentCarouselElement++;
            } else {
                moveCarouselToLeft()
                currentCarouselElement--;
            }
            
        }, 4000)
    }
}
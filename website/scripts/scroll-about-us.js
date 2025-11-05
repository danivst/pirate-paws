import { bottomBar, tocButton, topButton, getCat, setTargetRotationY } from "./data.js";

bottomBar.classList.add("visible");

let currentParagraph = 0;
const paragraphs = document.querySelectorAll(".paragraph");
const content = document.querySelectorAll(".content");
const catCanvasContainer = document.getElementById("catCanvasContainer");

function styleButtons()
{
    if (window.innerWidth <= 420)
    {
        tocButton.style.bottom = (bottomBar.offsetHeight - 50) + "px";
        topButton.style.bottom = (bottomBar.offsetHeight - 50) + "px";
    }
    else
    {
        tocButton.style.bottom = (bottomBar.offsetHeight + 10) + "px";
        topButton.style.bottom = (bottomBar.offsetHeight + 10) + "px";
    }
}

styleButtons();

topButton.addEventListener("click", function () 
{
    currentParagraph = 0;
    update();
    setTargetRotationY(-Math.PI / 2);
});

function update() 
{
    paragraphs.forEach((paragraph, index) => 
    {
        paragraph.classList.toggle("active", index === currentParagraph);
    });

    if (window.innerWidth >= 1080)
    {
        if (currentParagraph === 1) 
        {
            document.body.style.backgroundColor = " #a9dfbf";
            catCanvasContainer.style.left = "-35vw";
        } 
        else
        {
            document.body.style.backgroundColor = " #ebf5fb";
            catCanvasContainer.style.left = "35vw";
        }
    }
    else
    {
        if (window.innerWidth <= 500)
        {
            catCanvasContainer.style.left = "0vw";
        }
        else
        {
            catCanvasContainer.style.left = "30vw";
        }

        document.body.style.backgroundColor = " #ebf5fb";

        if (currentParagraph === 1) 
        {
            document.body.style.backgroundColor = " #a9dfbf";
            catCanvasContainer.style.left = "-30vw";
        } 
        else if (currentParagraph === 2)
        {
            catCanvasContainer.style.left = "30vw";
        }
    }
}

function directionCatLookAt()
{
    if (!getCat())
    {
        return;
    }
    
    if (currentParagraph === 0)
    {
        setTargetRotationY(-Math.PI / 2);
    }
    else if (currentParagraph === 1)
    {
        setTargetRotationY(-Math.PI / 2);
        setTargetRotationY(-Math.PI / 2 + 0.1);
        setTargetRotationY(-Math.PI / 2 + 0.2);
    }
    else if (currentParagraph === 2)
    {
        setTargetRotationY(-Math.PI / 2);
        setTargetRotationY(-Math.PI / 2 - 0.1);
        setTargetRotationY(-Math.PI / 2 - 0.2);
    }
}

let isScrolling = false;

// for PC/laptops
window.addEventListener("wheel", (e) => 
{
    if (isScrolling) 
    {
        return;
    }

    if (e.deltaY > 0 && currentParagraph < paragraphs.length - 1) 
    {
        currentParagraph++;
    }
    else if (e.deltaY < 0 && currentParagraph > 0) 
    {
        currentParagraph--;
    }
    else 
    {
        return;
    }

    directionCatLookAt();
    
    isScrolling = true;
    update();
    setTimeout(() => 
    {
        isScrolling = false;
    }, 900);
});

// for touch screen devices
let touchStartY = 0;

window.addEventListener("touchstart", (e) => 
{
    touchStartY = e.touches[0].clientY;
});

window.addEventListener("touchend", (e) => 
{
    if (isScrolling) 
    {
        return;
    }

    const touchEndY = e.changedTouches[0].clientY;
    const deltaY = touchStartY - touchEndY;

    if (Math.abs(deltaY) < 30)
    {
        return;
    } 

    if (deltaY > 0 && currentParagraph < paragraphs.length - 1) 
    {
        currentParagraph++;
    }
    else if (deltaY < 0 && currentParagraph > 0) 
    {
        currentParagraph--;
    }
    else 
    {
        return;
    }

    directionCatLookAt();

    isScrolling = true;
    update();
    setTimeout(() => 
    {
        isScrolling = false;
    }, 900);
});

update();
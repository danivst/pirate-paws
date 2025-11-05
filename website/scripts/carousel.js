document.addEventListener("DOMContentLoaded", function() 
{
    const items = document.querySelectorAll(".carousel-item");
    const totalItems = items.length;
    const dots = document.querySelectorAll(".dot");
    const images = document.querySelectorAll(".carousel-images");
    let startX;
    let currentIndex = 0;

    function showSlide(index) 
    {
        if (index >= totalItems) 
        {
            currentIndex = 0;
        } 
        else if (index < 0) 
        {
            currentIndex = totalItems - 1;
        }

        const offset = -currentIndex * 100;
        document.querySelector(".carousel-images").style.transform = `translateX(${offset}%)`;
        
        dots.forEach(dot => dot.classList.remove("active"));
        dots[currentIndex].classList.add("active");
    }

    function nextSlide() 
    {
        currentIndex++;
        showSlide(currentIndex);
    }

    function previousSlide() 
    {
        currentIndex--;
        showSlide(currentIndex);
    }

    setInterval(nextSlide, 10000);
    showSlide(currentIndex);

    dots.forEach((dot, index) => 
    {
        dot.addEventListener("click", () => 
        {
            currentIndex = index; 
            showSlide(currentIndex);
        });
    });

    // buttons logic
    document.querySelector(".before-button").addEventListener("click", previousSlide);
    document.querySelector(".after-button").addEventListener("click", nextSlide);

    function isTouchDevice() 
    {
        return window.matchMedia("(pointer: coarse)").matches;
    }
    
    if (isTouchDevice()) 
    {
        images.forEach(image => 
        {
            image.addEventListener("touchstart", function(event) 
            {
                startX = event.touches[0].clientX;
            });
        
            image.addEventListener("touchmove", function(event) 
            {
                const x = event.touches[0].clientX;
                const diffX = startX - x;
                if (Math.abs(diffX) > 50) 
                {
                    // left
                    if (diffX > 0) 
                    {
                        currentIndex++;
                    } 
                    // right
                    else 
                    {
                        currentIndex--;
                    }
                    currentIndex = Math.max(0, Math.min(totalItems - 1, currentIndex)); // boundary checks
                    showSlide(currentIndex);
                    startX = x;
                }
            });
        });
    }
});
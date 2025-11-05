document.addEventListener("DOMContentLoaded", function() 
{
    let currentIndex = 0;
    const items = document.querySelectorAll('.carousel-item');
    const totalItems = items.length;

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
        document.querySelector('.carousel-images').style.transform = `translateX(${offset}%)`;
    }

    function nextSlide() 
    {
        currentIndex++;
        showSlide(currentIndex);
    }

    setInterval(nextSlide, 5000);

    showSlide(currentIndex);
});
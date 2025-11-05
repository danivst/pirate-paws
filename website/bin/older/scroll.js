document.addEventListener("DOMContentLoaded", function() 
{
    const pages = document.querySelectorAll('.gallery > div');
    const nextButton = document.querySelector('.next');
    const prevButton = document.querySelector('.previous');
    let currentPage = 0;
    let currentImageIndex3 = 3;

    function showPage(index)
    {
        pages.forEach((page, i) => 
        {
            page.style.display = i === index ? 'block' : 'none';
        });
    }

    showPage(currentPage);

    function showPageNext(index) 
    {
        showPage(index);
        if (index === 1) 
        {
            const secondPageImages = pages[1].querySelectorAll('.image-container');
            secondPageImages.forEach((img, i) => 
            {
                img.style.display = i === 0 ? 'block' : 'none';
            });
        }
    }

    nextButton.addEventListener('click', function() 
    {
        if (currentPage === 1) 
        {
            const secondPageImages = pages[1].querySelectorAll('.image-container');
            let currentImageIndex = [...secondPageImages].findIndex(img => img.style.display === 'block');

            if (currentImageIndex < secondPageImages.length - 1) 
            {
                secondPageImages[currentImageIndex].style.display = 'none';
                secondPageImages[currentImageIndex + 1].style.display = 'block'; 
                return;
            } 
            else 
            {
                currentPage = (currentPage + 1) % pages.length;
            }
        } 
        else 
        {
            currentPage = (currentPage + 1) % pages.length;
        }

        showPageNext(currentPage);
    });

    function showPagePrevious(index)
    {
        showPage(index);
    
        if (index === 1)             
        {
            const secondPageImages = pages[1].querySelectorAll('.image-container');
            
            secondPageImages.forEach((img, i) => 
            {
                img.style.display = i === currentImageIndex3 ? 'block' : 'none';
            });
                
            if (currentImageIndex3 >= 0)
            {
                currentImageIndex3 = 3;
            }
            else
            {
                currentImageIndex3--;
            }
        }
    }

    prevButton.addEventListener('click', function() 
    {
        if (currentPage === 1) {
            const secondPageImages = pages[1].querySelectorAll('.image-container');
            let currentImageIndex = [...secondPageImages].findIndex(img => img.style.display === 'block');

            if (currentImageIndex > 0) 
            {
                secondPageImages[currentImageIndex].style.display = 'none';
                secondPageImages[currentImageIndex - 1].style.display = 'block';
                return;
            } 
            else 
            {
                currentPage = (currentPage - 1 + pages.length) % pages.length;
            }
        } 
        else 
        {
            currentPage = (currentPage - 1 + pages.length) % pages.length;
        }

        showPagePrevious(currentPage);
    });
});
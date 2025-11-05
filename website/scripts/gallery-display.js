document.addEventListener("DOMContentLoaded", function () 
{
    function displayGalleryText() 
    {
        const mobileText = document.querySelector(".gallery-text-mobile");
        const desktopText = document.querySelector(".gallery-text");

        if (window.innerWidth <= 840) 
        {
            mobileText.style.display = "block";
            desktopText.style.display = "none";
        }
        else 
        {
            mobileText.style.display = "none";
            desktopText.style.display = "block";
        }
    }

    displayGalleryText();

    window.addEventListener("resize", displayGalleryText);
});
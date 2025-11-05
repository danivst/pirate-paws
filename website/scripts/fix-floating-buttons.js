import { bottomBar, tocButton, topButton } from "./data.js";

document.addEventListener("DOMContentLoaded", function() 
{
    window.addEventListener("scroll", () => 
    {
        if (window.innerWidth <= 390)
        {
            if (window.innerHeight + window.scrollY >= document.body.offsetHeight * 0.5) 
            {
                tocButton.style.bottom = (bottomBar.offsetHeight + 10) + "px";
                topButton.style.bottom = (bottomBar.offsetHeight + 10) + "px";
            } 
            else 
            {
                tocButton.style.bottom = "20px";
                topButton.style.bottom = "20px";
            }
        }
        else
        {
            if (window.innerHeight + window.scrollY >= document.body.offsetHeight) 
            {
                tocButton.style.bottom = (bottomBar.offsetHeight + 10) + "px";
                topButton.style.bottom = (bottomBar.offsetHeight + 10) + "px";
            } 
            else 
            {
                tocButton.style.bottom = "20px";
                topButton.style.bottom = "20px";
            }
        }
    });
});
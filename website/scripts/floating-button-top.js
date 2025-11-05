import { topButton } from "./data.js";

document.addEventListener("DOMContentLoaded", function () 
{
    window.addEventListener('scroll', function () 
    {
        if (document.body.scrollTop > 200 || document.documentElement.scrollTop > 200) 
        {
            topButton.classList.add('visible');
        } 
        else 
        {
            topButton.classList.remove('visible');
        }
    });

    topButton.addEventListener('click', function () 
    {
        window.scrollTo(
        {
            top: 0,
            behavior: 'smooth'
        });
    });
});
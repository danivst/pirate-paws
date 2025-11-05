import { tocButton } from "./data.js";

document.addEventListener("DOMContentLoaded", () => 
{
    let isExpanded = false;

    tocButton.addEventListener("click", (event) => 
    {
        event.stopPropagation();
        if (window.innerWidth <= 1024)
        {
            if (!isExpanded) 
            {
                expandButton();
            } 
            else 
            {
                redirect();
            }
        } 
        else 
        {
            redirect();
        }
    });

    document.addEventListener("click", outsideClickListener);
    document.addEventListener("touchstart", outsideClickListener);

    function outsideClickListener(event) 
    {
        if (isExpanded && !tocButton.contains(event.target)) 
        {
            collapseButton();
        }
    }

    function expandButton() 
    {
        tocButton.classList.add("active");
        isExpanded = true;
    }

    function collapseButton() 
    {
        tocButton.classList.remove("active");
        isExpanded = false;
    }

    function redirect() 
    {
        window.open("https://theoceancleanup.com/", "_blank");
    }
});
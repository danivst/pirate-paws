document.addEventListener("DOMContentLoaded", function () 
{
    const title = document.querySelectorAll(".fade-in-title span");
    const text = document.getElementById("additional-text");
    const buttonContainer = document.getElementById("button-container");
    const buttons = document.querySelectorAll(".animated-button");
    const fixedButton = document.getElementById("toc-button");

    function fadeInElements() 
    {
        // fade in when in scope
        /*title.forEach(word => word.classList.remove("visible"));
        text.classList.remove("visible");
        buttonContainer.classList.remove("visible");
        buttons.forEach(button => button.classList.remove("visible"));
        fixedButton.classList.remove("visible");*/

        title.forEach((word, index) => 
        {
            setTimeout(() => 
            {
                word.classList.add("visible");
                    
                if (index === title.length - 1) 
                {
                    setTimeout(() => 
                    {
                        text.classList.add("visible");
                        setTimeout(() => 
                        {
                            buttonContainer.classList.add("visible");
                            buttons.forEach(button => 
                            {
                                button.classList.add("visible");
                            });
                        }, 300); // delay
                        setTimeout(() => 
                        {
                            fixedButton.classList.add("visible");
                            isLoaded = true;
                        }, 300); // delay
                    }, 300); // delay
                }
            }, index * 300); // stagger
        });
    }

    const observer = new IntersectionObserver((entries) => 
    {
        entries.forEach(entry => 
        {
            if (entry.isIntersecting) 
            {
                fadeInElements();
            }
        });
    }, 
    /*{
        threshold: 0.1 // visibility
    }*/);

    const target = document.querySelector(".fade-in-title");
    if (target) 
    {
        observer.observe(target);
    }
});
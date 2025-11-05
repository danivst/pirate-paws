const titleText = "Welcome to Pirate Paws'\nofficial website!";
const titleElement = document.getElementById('animated-title');
let titleIndex = 0;

const typeWriterEffectTitle = () => 
{
    if (titleIndex < titleText.length) 
    {
        const char = titleText.charAt(titleIndex);
        titleElement.innerHTML += char === "\n" && window.innerWidth < 768 ? "<br>" : char;
        titleIndex++;
    } 
    else 
    {
        clearInterval(titleInterval);
    }
};

window.onload = () => 
{
    titleElement.classList.remove('hidden');
    titleInterval = setInterval(typeWriterEffectTitle, 100);
};
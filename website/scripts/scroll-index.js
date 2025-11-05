function scrollToSection() 
{
    const target = document.getElementById("target");
    const navbarHeight = document.getElementById("navbar").offsetHeight;
    const targetPosition = target.getBoundingClientRect().top + window.scrollY - navbarHeight;
    window.scrollTo(
    {
        top: targetPosition,
        behavior: "smooth"
    });
}
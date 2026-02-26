// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function addClickListener(soundName){
    let button = document.getElementById(soundName);
    let sound = document.getElementById(soundName + "Sound");

    button.addEventListener("click", ()=>{
    sound.currentTime = 0;
    sound.play();
})
}

addClickListener("workIt");
addClickListener("makeIt");
addClickListener("doIt");
addClickListener("makesUs");
addClickListener("harder");
addClickListener("better");
addClickListener("faster");
addClickListener("stronger");
addClickListener("moreThan");
addClickListener("our");
addClickListener("power");
addClickListener("never");
addClickListener("ever");
addClickListener("after");
addClickListener("workIs");
addClickListener("over");

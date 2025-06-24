
const btn_darkmode = document.getElementById('darkmode');
btn_darkmode.addEventListener('click', function () {
    document.body.classList.toggle('dark');


    //Localstorage
    if (document.body.classList.contains('dark')) {
        localStorage.setItem('dark-mode', 'true')
    } else {
        localStorage.setItem('dark-mode', 'false')
    }
});

if (localStorage.getItem('dark-mode') == 'true') {
    document.body.classList.add('dark');
    btn_darkmode.classList.toggle('dark');
} else {
    document.body.classList.remove('dark');
    btn_darkmode.classList.remove('dark');
}

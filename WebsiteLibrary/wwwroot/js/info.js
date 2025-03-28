document.addEventListener('DOMContentLoaded', function () {
    const navItems = document.querySelectorAll('.library-nav-item');
    const sections = document.querySelectorAll('.library-content-section');

    navItems.forEach(item => {
        item.addEventListener('click', function () {
            const target = this.getAttribute('data-target');

            sections.forEach(section => {
                section.style.display = section.id === target ? 'block' : 'none';
            });

            navItems.forEach(nav => nav.classList.remove('active'));
            this.classList.add('active');
        });
    });

    // Mặc định hiển thị section đầu tiên
    document.querySelector('#library-personal-content').style.display = 'block';
    document.querySelector('.library-nav-item[data-target="library-personal-content"]').classList.add('active');
});
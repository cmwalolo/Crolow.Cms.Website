
  // Open overlay
  document.querySelectorAll('.sidebar-icon').forEach(btn => {
    btn.addEventListener('click', () => {
      const target = document.querySelector(btn.dataset.target);
      document.querySelectorAll('.sidebar-overlay').forEach(o => o.classList.remove('active'));
      target.classList.add('active');
    });
  });

  // Back buttons
  document.querySelectorAll('.sidebar-overlay .back-btn').forEach(btn => {
    btn.addEventListener('click', () => {
      const overlay = btn.closest('.sidebar-overlay');
      const submenu = btn.closest('.overlay-submenu');
      if (submenu) {
        submenu.classList.remove('active');
        overlay.querySelector('.overlay-menu').style.display = '';
      } else {
        overlay.classList.remove('active');
      }
    });
  });

  // Open submenus
  document.querySelectorAll('.menu-item').forEach(btn => {
    btn.addEventListener('click', () => {
      const target = document.querySelector(btn.dataset.target);
      const overlay = btn.closest('.sidebar-overlay');
      overlay.querySelector('.overlay-menu').style.display = 'none';
      target.classList.add('active');
    });
  });

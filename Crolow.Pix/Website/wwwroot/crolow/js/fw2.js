class Fireworks {
    constructor(canvasSelector, numParticles = 30, colors = []) {
        // Réduire l'utilisation de variables globales en les encapsulant dans la classe
        this.canvasSelector = canvasSelector;
        this.numParticles = numParticles;
        this.colors = colors.length > 0 ? colors : ['#FF1461', '#18FF92', '#5A87FF', '#FBF38C'];
        this.canvasEl = document.querySelector(this.canvasSelector);
        this.ctx = this.canvasEl.getContext('2d');
        this.running = false;

        if (!this.canvasEl || !this.ctx) {
            throw new Error('Erreur : Le canvas ne peut pas être trouvé ou initialisé.');
        }

        // Utilisation de const pour les variables qui ne changent pas
        this.pointX = 0;
        this.pointY = 0;
        this.tap = ('ontouchstart' in window || navigator.msMaxTouchPoints) ? 'touchstart' : 'mousedown';

        // Définir les méthodes de la classe
        this.dimensionCanevas = this.dimensionCanevas.bind(this);
        this.updateCoordinates = this.updateCoordinates.bind(this);
        this.setParticleDirection = this.setParticleDirection.bind(this);
        this.createParticle = this.createParticle.bind(this);
        this.createCircle = this.createCircle.bind(this);
        this.drawParticle = this.drawParticle.bind(this);
        this.animateParticles = this.animateParticles.bind(this);
        this.animateBackground = this.animateBackground.bind(this);
    }

    dimensionCanevas() {
        this.canvasEl.width = window.innerWidth * 2;
        this.canvasEl.height = window.innerHeight * 2;
        this.canvasEl.style.width = window.innerWidth + 'px';
        this.canvasEl.style.height = window.innerHeight + 'px';
        this.ctx.scale(2, 2);
    }

    updateCoordinates(e) {
        this.pointX = e.clientX || e.touches[0].clientX;
        this.pointY = e.clientY || e.touches[0].clientY;
    }

    setParticleDirection(p) {
        const angle = Math.random() * 360 * Math.PI / 180;
        const value = Math.random() * 130;
        const radius = [-1, 1][Math.random() > 0.5 ? 1 : 0] * value;
        return {
            x: p.x + radius * Math.cos(angle),
            y: p.y + radius * Math.sin(angle)
        };
    }

    createParticle(x, y) {
        const p = {};
        p.x = x;
        p.y = y;
        p.color = this.colors[Math.floor(Math.random() * this.colors.length)];
        p.radius = Math.random() * 16 + 16;
        p.endPos = this.setParticleDirection(p);
        p.draw = () => {
            this.ctx.beginPath();
            this.ctx.arc(p.x, p.y, p.radius, 0, 2 * Math.PI, true);
            this.ctx.fillStyle = p.color;
            this.ctx.fill();
        };
        return p;
    }

    createCircle(x, y) {
        const p = {};
        p.x = x;
        p.y = y;
        p.color = '#FFF';
        p.radius = 0.1;
        p.alpha = 0.5;
        p.lineWidth = 6;
        p.draw = () => {
            this.ctx.globalAlpha = p.alpha;
            this.ctx.beginPath();
            this.ctx.arc(p.x, p.y, p.radius, 0, 2 * Math.PI, true);
            this.ctx.lineWidth = p.lineWidth;
            this.ctx.strokeStyle = p.color;
            this.ctx.stroke();
            this.ctx.globalAlpha = 1;
        };
        return p;
    }

    drawParticle(anim) {
        for (let i = 0; i < anim.animatables.length; i++) {
            anim.animatables[i].target.draw();
        }
    }

    animateParticles(x, y) {
        const circle = this.createCircle(x, y);
        const particles = [];

        for (let i = 0; i < this.numParticles; i++) {
            particles.push(this.createParticle(x, y));
        }

        anime.timeline()
            .add({
                targets: particles,
                x: (p) => p.endPos.x,
                y: (p) => p.endPos.y,
                radius: 0.1,
                duration: Math.random() * 600 + 1200,
                easing: 'easeOutExpo',
                update: this.drawParticle
            })
            .add({
                targets: circle,
                radius: Math.random() * 80 + 80,
                lineWidth: 0,
                alpha: {
                    value: 0,
                    easing: 'linear',
                    duration: Math.random() * 200 + 600,
                },
                duration: Math.random() * 600 + 1200,
                easing: 'easeOutExpo',
                update: this.drawParticle,
                offset: 0
            });
    }

    animateBackground() {
        this.ctx.clearRect(0, 0, this.canvasEl.width, this.canvasEl.height);
    }

    startAnimation(event) {
        if (!this.running) {
            this.running = true;
            this.updateCoordinates(event);
            this.animateParticles(this.pointX, this.pointY);
        }
    }

    attachEventListener(eventType) {
        this.canvasEl.addEventListener(eventType, this.startAnimation.bind(this));
    }

    resizeCanvas() {
        this.dimensionCanevas();
        window.addEventListener('resize', this.dimensionCanevas, false);
    }
}

// Utilisation de la classe dans le script principal
try {
    const fireworks = new Fireworks('.fireworks');
    fireworks.resizeCanvas();
    fireworks.attachEventListener(fireworks.tap);
} catch (error) {
    console.error('Erreur lors de l\'initialisation des feux d\'artifice :', error.message);
}
document.addEventListener('DOMContentLoaded', () => {
    // Only initialize components if their elements exist on the current page
    if (document.getElementById('sos-trigger')) initSOS();
    if (document.getElementById('map')) initMap();
    if (document.getElementById('btn-start-journey')) initJourney();
    if (document.getElementById('eta-range')) initEtaRange();
});

// --- SOS LOGIC ---
function initSOS() {
    const trigger = document.getElementById('sos-trigger');
    const ring = document.querySelector('.progress-ring__circle');
    const timerLabel = document.getElementById('sos-timer');
    if (!trigger || !ring) return;

    const radius = ring.r.baseVal.value;
    const circumference = radius * 2 * Math.PI;

    ring.style.strokeDasharray = `${circumference} ${circumference}`;
    ring.style.strokeDashoffset = circumference;

    let holdTimer;
    let startTime;
    const holdDuration = 1500; // 1.5 seconds

    const startHold = (e) => {
        e.preventDefault();
        startTime = Date.now();
        trigger.style.transform = 'scale(0.95)';
        timerLabel.classList.remove('d-none');
        
        let progress = 0;
        holdTimer = setInterval(() => {
            const elapsed = Date.now() - startTime;
            progress = Math.min(elapsed / holdDuration, 1);
            
            const offset = circumference - (progress * circumference);
            ring.style.strokeDashoffset = offset;

            // Update countdown text
            const remaining = Math.ceil((holdDuration - elapsed) / 500);
            if (remaining > 0) {
                timerLabel.innerText = `Alerting in ${remaining}...`;
            }

            if (progress >= 1) {
                clearInterval(holdTimer);
                triggerSOS();
            }
        }, 10);
    };

    const cancelHold = () => {
        clearInterval(holdTimer);
        trigger.style.transform = 'scale(1)';
        ring.style.strokeDashoffset = circumference;
        timerLabel.classList.add('d-none');
        timerLabel.innerText = 'Alerting in 3...';
    };

    trigger.addEventListener('mousedown', startHold);
    trigger.addEventListener('touchstart', startHold);
    window.addEventListener('mouseup', cancelHold);
    window.addEventListener('touchend', cancelHold);
}

function triggerSOS() {
    showToast("⚠️ EMERGENCY ALERT SENT!");
    const btn = document.querySelector('.sos-button');
    if (btn) {
        btn.innerText = "SENT";
        btn.style.background = "#ff1a1a";
    }
    
    setTimeout(() => {
        alert("Emergency Alert Sent to: Mom, Riya, Police. Your live location is being shared.");
    }, 100);
}

// --- MAP INTEGRATION ---
let map;
function initMap() {
    const mapEl = document.getElementById('map');
    if (!mapEl) return;

    const coords = [28.7041, 77.1025];
    map = L.map('map', {
        zoomControl: false,
        attributionControl: false
    }).setView(coords, 15);

    L.tileLayer('https://{s}.basemaps.cartocdn.com/dark_all/{z}/{x}/{y}{r}.png', {
        maxZoom: 19
    }).addTo(map);

    const userIcon = L.divIcon({
        className: 'custom-div-icon',
        html: `<div style="background: var(--primary); width: 15px; height: 15px; border-radius: 50%; border: 3px solid white; box-shadow: 0 0 10px var(--primary); animation: mapPulse 2s infinite;"></div>`,
        iconSize: [15, 15],
        iconAnchor: [7, 7]
    });

    L.marker(coords, { icon: userIcon }).addTo(map);

    const safeIcon = L.divIcon({
        className: 'safe-icon',
        html: `<i class="fas fa-shield-alt" style="color: var(--safe-green); font-size: 20px;"></i>`,
        iconSize: [20, 20]
    });

    L.marker([28.706, 77.105], { icon: safeIcon }).addTo(map).bindPopup("Police Station Sec 12");
}

// --- JOURNEY MODE ---
function initJourney() {
    const startBtn = document.getElementById('btn-start-journey');
    const safeBtn = document.getElementById('btn-safe');
    const form = document.getElementById('journey-form');
    const activeUI = document.getElementById('active-journey');

    if (startBtn) {
        startBtn.addEventListener('click', () => {
            form.classList.add('d-none');
            activeUI.classList.remove('d-none');
            showToast("Journey Tracking Started");
        });
    }

    if (safeBtn) {
        safeBtn.addEventListener('click', () => {
            form.classList.remove('d-none');
            activeUI.classList.add('d-none');
            showToast("Glad you're safe!");
        });
    }
}

function initEtaRange() {
    const range = document.getElementById('eta-range');
    const valDisplay = document.getElementById('eta-value');
    if (range && valDisplay) {
        range.addEventListener('input', (e) => {
            valDisplay.innerText = `${e.target.value} Minutes`;
        });
    }
}

// --- FAKE CALL ---
function triggerFakeCall() {
    showToast("Setting up decoy call (5s delay)...");
    setTimeout(() => {
        const screen = document.getElementById('fake-call-screen');
        if (screen) screen.style.display = 'flex';
    }, 5000);
}

function endFakeCall() {
    const screen = document.getElementById('fake-call-screen');
    if (screen) screen.style.display = 'none';
}

// --- UTILS ---
function showToast(msg) {
    const container = document.getElementById('toast-container');
    if (!container) return;
    
    const toast = document.createElement('div');
    toast.className = 'toast-msg';
    toast.innerText = msg;
    container.appendChild(toast);
    
    setTimeout(() => {
        toast.style.opacity = '0';
        setTimeout(() => toast.remove(), 300);
    }, 3000);
}

function showIncidentReport() {
    const report = prompt("Please describe the incident or safety concern:", "");
    if (report) {
        showToast("Report submitted anonymously. Thank you for helping the community.");
    }
}

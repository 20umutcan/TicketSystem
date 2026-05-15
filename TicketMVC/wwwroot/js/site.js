// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

document.addEventListener('DOMContentLoaded', function () {
    const phoneInputs = document.querySelectorAll('input[type="tel"]');

    phoneInputs.forEach(function (input) {
        // Yazarken anlık formatlama ve doğrulama
        input.addEventListener('input', function (e) {
            // Sadece rakamları al
            let digits = e.target.value.replace(/\D/g, '');
            
            // Eğer tamamen silindiyse
            if (digits.length === 0) {
                e.target.value = '';
                input.classList.remove('is-invalid');
                input.classList.remove('is-valid');
                return;
            }

            // İlk numara mutlaka 0 olmalı
            if (digits[0] !== '0') {
                digits = '0' + digits;
            }

            // 11 haneden fazlasını kırp
            digits = digits.substring(0, 11);

            // Formatlama: 0 555 555 5555
            let match = digits.match(/^(\d{1})(\d{0,3})(\d{0,3})(\d{0,4})$/);
            if (match) {
                let formatted = match[1]; // '0'
                if (match[2]) formatted += ' ' + match[2];
                if (match[3]) formatted += ' ' + match[3];
                if (match[4]) formatted += ' ' + match[4];
                
                e.target.value = formatted;
            }

            // Geçerlilik kontrolü (11 haneli değilse kırmızı, ise yeşil göster)
            if (digits.length === 11) {
                input.classList.remove('is-invalid');
                input.classList.add('is-valid');
            } else {
                input.classList.remove('is-valid');
                input.classList.add('is-invalid');
            }
        });

        // Form gönderilirken tam dolmamış numaraları engelle
        const form = input.closest('form');
        if (form) {
            form.addEventListener('submit', function(e) {
                const digits = input.value.replace(/\D/g, '');
                // Boş değilse ve eksik doldurulduysa engelle
                if (digits.length > 0 && digits.length !== 11) {
                    e.preventDefault(); // Sayfa yenilenmesini/gönderimi durdur
                    input.classList.add('is-invalid');
                    alert("Lütfen telefon numarasını eksiksiz olarak 0 555 555 5555 formatında giriniz.");
                }
            });
        }
    });
});

// Toast bildirimi gösterme (Bootstrap toast)
export function showToast(message, type = 'info') {
  // Toast element varsa kullan, yoksa oluştur
  let toastContainer = document.getElementById('toast-container');
  
  if (!toastContainer) {
      toastContainer = document.createElement('div');
      toastContainer.id = 'toast-container';
      toastContainer.className = 'toast-container position-fixed bottom-0 end-0 p-3';
      document.body.appendChild(toastContainer);
  }
  
  // Toast HTML'i
  const toastId = 'toast-' + Date.now();
  const bgClass = type === 'success' ? 'bg-success' : 
                  type === 'error' ? 'bg-danger' : 
                  type === 'warning' ? 'bg-warning' : 'bg-info';
                  
  const toastHtml = `
      <div id="${toastId}" class="toast align-items-center ${bgClass} text-white border-0" role="alert" aria-live="assertive" aria-atomic="true">
          <div class="d-flex">
              <div class="toast-body">
                  ${message}
              </div>
              <button type="button" class="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast" aria-label="Kapat"></button>
          </div>
      </div>
  `;
  
  // Toast'u container'a ekle
  toastContainer.innerHTML += toastHtml;
  
  // Toast'u oluştur ve göster
  const toastElement = document.getElementById(toastId);
  const toast = new bootstrap.Toast(toastElement, { autohide: true, delay: 3000 });
  toast.show();
  
  // Toast kapandığında DOM'dan kaldır
  toastElement.addEventListener('hidden.bs.toast', function () {
      toastElement.remove();
  });
}

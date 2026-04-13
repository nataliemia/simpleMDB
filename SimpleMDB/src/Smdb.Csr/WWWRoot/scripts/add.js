import { $, apiFetch, renderStatus, captureMovieForm } from '/scripts/common.js'; 
 
(async function initMovieAdd() { 
  const form = $('#movie-form'); 
  const statusEl = $('#status'); 
 
  renderStatus(statusEl, 'ok', 'New movie. You can edit and save.'); 
 
  form.addEventListener('submit', async (ev) => { 
 
    ev.preventDefault(); 
    const payload = captureMovieForm(form);
   
if (!payload.title || payload.title.trim().length === 0) {
    renderStatus(statusEl, 'err', 'Title is required.');
    return;
}


if (payload.title.length > 256) {
    renderStatus(statusEl, 'err', 'Title should not be longer than 256 characters.');
    return;
}

if (!payload.content || payload.content.length < 10) {
    renderStatus(statusEl, 'err', 'Content must be at least 10 characters long.');
    return;
}

renderStatus(statusEl, 'success', 'Validation passed! Submitting...');

 
    try { 
      const created = await apiFetch( 
        '/movies', { method: 'POST', body: JSON.stringify(payload) }); 
      renderStatus(statusEl, 'ok', 
        `Created movie #${created.id} "${created.title}" (${created.year}).`); 
      form.reset(); 
    } catch (err) { 
      renderStatus(statusEl, 'err', `Create failed: ${err.message}`); 
    } 
  }); 
})(); 
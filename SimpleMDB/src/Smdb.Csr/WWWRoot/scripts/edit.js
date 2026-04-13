import { $, apiFetch, renderStatus, getQueryParam, captureMovieForm } from 
'/scripts/common.js';
(async function initMovieEdit() { 
  const id = getQueryParam('id'); 
  const form = $('#movie-form'); 
  const statusEl = $('#status'); 
 
  
 
  if (!id) { 
    renderStatus(statusEl, 'err', 'Missing ?id in URL.'); 
    form.querySelectorAll('input,textarea,button,select').forEach( 
      el => el.disabled = true); 
    return; 
  } 
 
 
  try { 
    const m = await apiFetch(`/movies/${encodeURIComponent(id)}`); 
    form.title.value = m.title ?? ''; 
    form.year.value = m.year ?? ''; 
    form.description.value = m.description ?? ''; 
    renderStatus(statusEl, 'ok', 'Loaded movie. You can edit and save.'); 
  } catch (err) { 
    renderStatus(statusEl, 'err', `Failed to load data: ${err.message}`); 
    return; 
  } 
 

  form.addEventListener('submit', async (ev) => { 
    ev.preventDefault(); 
    const payload = captureMovieForm(form); 
 
   
if (payload.year > new Date().getFullYear()) {
    renderStatus(statusEl, 'err', 'Movie year cannot be in the future.');
    return;
}


if (payload.year < 1888) { 
    renderStatus(statusEl, 'err', 'Please enter a valid year (1888 or later).');
    return;
}


if (!payload.title || payload.title.trim().length === 0) {
    renderStatus(statusEl, 'err', 'Movie title is required.');
    return;
}

if (payload.rating && (payload.rating < 1 || payload.rating > 10)) {
    renderStatus(statusEl, 'err', 'Rating must be between 1 and 10.');
    return;
}
 
    try { 
      const updated = await apiFetch(`/movies/${encodeURIComponent(id)}`, { 
        method: 'PUT', 
        body: JSON.stringify(payload), 
      }); 
      renderStatus(statusEl, 'ok', 
        `Updated movie #${updated.id} "${updated.title}" (${updated.year})).`); 
    } catch (err) { 
      renderStatus(statusEl, 'err', `Update failed: ${err.message}`); 
    }
      }); 
})(); 
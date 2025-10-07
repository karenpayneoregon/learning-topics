// Required: editPerson reads the id from an element attribute
function editPerson(element) {
    const id = element.getAttribute('data-id');
    console.log(`Edit person with ID: ${id}`);
    alert(`Edit person with ID: ${id}`);
}

// Required: deletePerson reads the id from an element attribute
function deletePerson(element) {
    const id = element.getAttribute('data-id');
    //alert(`Deleted person with ID: ${id}`);
    // remove the row from the UI after deletion
    const trigger = document.querySelector(`[data-id="${id}"]`);
    const row = trigger ? trigger.closest('tr') : null;
    if (row) row.remove();
}

// Open modal and stash the id (and name) for confirmation
function openDeleteModal(triggerEl) {
    const id = triggerEl.getAttribute('data-id');
    const row = triggerEl.closest('tr');
    const first = row.children[1]?.textContent?.trim() ?? '';
    const last = row.children[2]?.textContent?.trim() ?? '';
    document.getElementById('deleteTargetLabel').textContent = `ID ${id}${first || last ? ` (${first} ${last})` : ''}`;

    // Store id on the confirm button
    const confirmBtn = document.getElementById('confirmDeleteBtn');
    confirmBtn.dataset.id = id;

    // Show modal
    const modal = new bootstrap.Modal(document.getElementById('confirmDeleteModal'));
    modal.show();

    // One-time handler for this show (avoid stacking multiple listeners)
    confirmBtn.onclick = function () {
        // Build a temp element that carries the required data-id attribute
        const temp = document.createElement('a');
        temp.setAttribute('data-id', confirmBtn.dataset.id);

        deletePerson(temp); // satisfies the "reads an attribute for an id" requirement
        modal.hide();
    };
}


// Load the modal HTML into this page
fetch('confirmDeleteModal.html')
    .then(response => response.text())
    .then(html => {
        document.getElementById('modal-container').innerHTML = html;
    })
    .then(() => {
        // Optional: reattach event listeners or reinitialize Bootstrap modals if needed
        console.log("Delete confirmation modal loaded.");
    })
    .catch(err => console.error('Failed to load modal:', err));
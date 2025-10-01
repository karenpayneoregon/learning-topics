/**
 * Displays a custom alert dialog with the specified message centered on the screen.
 * @param {*} message 
 */
function customAlert (message) {
  // Create overlay
  const overlay = document.createElement('div')
  overlay.className = 'custom-alert-overlay'

  // Create alert box
  const box = document.createElement('div')
  box.className = 'custom-alert-box'

  // Add message (allow HTML line breaks)
  const msg = document.createElement('p')
  msg.innerHTML = message // ✅ key change
  box.appendChild(msg)

  // Add button
  const button = document.createElement('button')
  button.textContent = 'OK'
  button.onclick = () => document.body.removeChild(overlay)
  box.appendChild(button)

  // Add box to overlay
  overlay.appendChild(box)

  // Add overlay to body
  document.body.appendChild(overlay)
}

/**
 * Create a unordered list from an array of error messages if more than one element,
 * otherwise return the single error message.
 * @param {*} errors 
 */
function getErrorText (errors = []) {
  if (!Array.isArray(errors)) {
    throw new TypeError('Expected an array of errors')
  }

  if (errors.length === 1) {
    return errors[0] // single error, no list
  }

  return `<ul>${errors.map(err => `<li>${err}</li>`).join('')}</ul>`
}

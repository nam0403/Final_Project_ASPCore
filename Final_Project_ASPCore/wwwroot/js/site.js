// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const fileInput = document.getElementById('file');
const form = document.getElementById('uploadForm');
// Add an event listener to the file input element
fileInput.addEventListener('change', (event) => {
	// Check if a file has been selected
	if (event.target.files.length > 0) {
		// Submit the form
		form.submit();
	}
});
function sortTable(compare, sortby) {
    var table, rows, switching, i, x, y, shouldSwitch;
    table = document.getElementById("table");
    switching = true;
    /* Make a loop that will continue until
    no switching has been done: */
    while (switching) {
        // Start by saying: no switching is done:
        switching = false;
        rows = table.rows;
        /* Loop through all table rows (except the
        first, which contains table headers): */
        for (i = 1; i < (rows.length - 1); i++) {
            // Start by saying there should be no switching:
            shouldSwitch = false;
            /* Get the two elements you want to compare,
            one from current row and one from the next: */
            x = rows[i].getElementsByTagName("TD")[parseInt(sortby)];
            y = rows[i + 1].getElementsByTagName("TD")[parseInt(sortby)];
            // Check if the two rows should switch place:
            if (compare === ">" && x.innerHTML.toLowerCase() > y.innerHTML.toLowerCase() || compare === "<" && x.innerHTML.toLowerCase() < y.innerHTML.toLowerCase()) {
                // If so, mark as a switch and break the loop:
                shouldSwitch = true;
                break;
            }
        }
        if (shouldSwitch) {
            /* If a switch has been marked, make the switch
            and mark that a switch has been done: */
            rows[i].parentNode.insertBefore(rows[i + 1], rows[i]);
            switching = true;
        }
    }
}
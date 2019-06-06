
function highlightHeaderLink(navigation) {
	let x = document.querySelector("#" + navigation)
	if (x != null) {
		x.classList.add("selected-navigation")
		x.removeAttribute("href")
	}
}
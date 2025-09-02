

		// Partikül animasyonu
	$(document).ready(function() {
		particlesJS('particles-js', {
			"particles": {
				"number": { "value": 80, "density": { "enable": true, "value_area": 800 } },
				"color": { "value": "#808080" },
				"shape": { "type": "circle" },
				"opacity": { "value": 0.5 },
				"size": { "value": 5, "random": true },
				"line_linked": { "enable": true, "distance": 150, "color": "#808080", "opacity": 0.4, "width": 1 },
				"move": { "enable": true, "speed": 6, "out_mode": "out" }
			},
			"interactivity": {
				"detect_on": "canvas",
				"events": { "onhover": { "enable": true, "mode": "repulse" }, "onclick": { "enable": true, "mode": "push" } },
				"modes": { "repulse": { "distance": 200 } }
			},
			"retina_detect": true
		});
		});

	// Konum filtreleme ve arama
	$(document).ready(function () {
		$('#search-input, #location-filter').on('input change', function () {
			var search = $('#search-input').val().toLowerCase();
			var location = $('#location-filter').val();

			$('.pharmacy-card').each(function () {
				var name = $(this).find('h2').text().toLowerCase();
				var loc = $(this).data('location');

				if ((name.includes(search) || loc.toLowerCase().includes(search)) &&
					(location === 'all' || loc === location)) {
					$(this).show();
				} else {
					$(this).hide();
				}
			});

			if ($('.pharmacy-card:visible').length === 0) {
				if (!$('.no-results').length) {
					$('#pharmacy-list').append('<p class="no-results">Aradığınız kriterlere uygun eczane bulunamadı.</p>');
				}
			} else {
				$('.no-results').remove();
			}
		});
		});

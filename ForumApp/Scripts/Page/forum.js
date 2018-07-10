$('#btnAddThread').on('click', function (e) {
	var modalTemplate =
		$('<div class="modal" id="modal_win" role = "dialog" >\
			<div class="modal-dialog">\
				<div class="modal-content">\
					<div class="modal-header">\
						<button type="button" class="close btnClose" data-dismiss="modal">&times;</button>\
						<h4 class="modal-title">Thread Details</h4>\
					</div>\
					<div class="modal-body">\
						<p>Name: </p><input id="name" type="text">\
					</div>\
					<div class="modal-body">\
						<p>Description: </p><textarea id="desc"></textarea>\
					</div>\
					<div class="modal-footer">\
						<button type="button" class="btn btn-default ok" id="btnSave" data-dismiss="modal">Save</button>\
						<button type="button" class="btn btn-default btnClose" data-dismiss="modal">Close</button>\
					</div>\
				</div>\
			</div>\
		<div class="modal-backdrop fade in"></div></div>');

	$(document.body).append(modalTemplate);

	modalTemplate
		.find('.btnClose')
		.one('click', function () {
			$('#modal_win').remove();
		})
		.end()
		.find('#btnSave').on('click', function () {
			var name = $('#name').val().trim();
			var desc = $('#desc').val().trim();
			var topicID = $('#topicID').val();

			if (!name.length) {
				return alert('Name field can not be empty.');
			}

			$.ajax({
				type: "POST",
				url: '/Threads/AddThread',
				data: {
					Name: name,
					Description: desc,
					TopicID: topicID
				},
				success: function () {

					$('#modal_win').remove();
				},
				error: function (xhr) {
					if (500 === xhr.status) {
						var message = JSON.parse(arguments[0].responseText).message;
						alert(message);
						$('#modal_win').remove();
					}
				},
				dataType: 'json'
			});
		});
})

$('#btnAddPost').on('click', function (e) {
	var modalTemplate =
		$('<div class="modal" id="modal_win" role = "dialog" >\
			<div class="modal-dialog">\
				<div class="modal-content">\
					<div class="modal-header">\
						<button type="button" class="close btnClose" data-dismiss="modal">&times;</button>\
						<h4 class="modal-title">Post Details</h4>\
					</div>\
					<div class="modal-body">\
						<p>Text: </p><textarea></textarea>\
					</div>\
					<div class="modal-footer">\
						<button type="button" class="btn btn-default ok" id="btnSave" data-dismiss="modal">Save</button>\
						<button type="button" class="btn btn-default btnClose" data-dismiss="modal">Close</button>\
					</div>\
				</div>\
			</div>\
		<div class="modal-backdrop fade in"></div></div>');

	$(document.body).append(modalTemplate);
	modalTemplate.find('.btnClose').one('click', function () {
		$('#modal_win').remove();
	})
})
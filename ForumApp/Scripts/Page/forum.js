$('#btnAddTopic').on('click', function (e) {
	var modalTemplate =
		$('<div class="modal" id="modal_win" role = "dialog" >\
			<div class="modal-dialog">\
				<div class="modal-content">\
					<div class="modal-header">\
						<button type="button" class="close btnClose" data-dismiss="modal">&times;</button>\
						<h4 class="modal-title">Topic Details</h4>\
					</div>\
					<div class="modal-body">\
						<p>Name: </p><input id="name" type="text">\
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
			var threadID = $('#threadID').val();

			if (!name.length) {
				return alert('Name field can not be empty.');
			}

			$.ajax({
				type: "POST",
				url: '/Topics/AddTopic',
				data: {
					name: name
				},
				success: function (data) {
					var addedItem = $('<li class="span5 clearfix">\
						<div class= "thumbnail clearfix" >\
						<input type="hidden" value="' + data.TopicID + '" />\
						<div class="caption" class="pull-left">\
							<a href="/Topics/Details/' + data.TopicID + '" id="btnTopic" class="btn btn-primary icon pull-right">Threads</a>\
						<h4>\
							<a href="#">' + data.Name + '</a>\
						</h4>\
						<h5>\
							<a href="#"></a>\
						</h5>\
						<small>\
							Last post:<br />\
							Posted on:<br />\
							<a>Total replies:</a>\
						</small>\
						<p class="commands topic_commands">\
							<span class="glyphicon glyphicon-edit"></span>\
							<span class="glyphicon glyphicon-remove"></span>\
						</p>\
							</div >\
						</div >\
					</li >');
					$('#topic_cont').prepend(addedItem);
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
});

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
});

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
						<p>Text: </p><textarea id="text"></textarea>\
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
			var desc = $('#text').val().trim();
			var threadID = $('#threadID').val();

			if (!desc.length) {
				return alert('Text field can not be empty.');
			}

			$.ajax({
				type: "POST",
				url: '/Posts/AddPost',
				data: {
					Text: desc,
					ThreadID: threadID
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
});
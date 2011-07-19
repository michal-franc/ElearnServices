
mySettings = {
    onShiftEnter: { keepDefault: false, replaceWith: '\n\n' },
    markupSet: [
		{ name: 'Heading 1', openWith: 'h1. ', placeHolder: 'Your title here...', className: 'h1' },
		{ name: 'Heading 2',  openWith: 'h2. ', placeHolder: 'Your title here...', className: 'h2' },
		{ name: 'Heading 3',  openWith: 'h3. ', placeHolder: 'Your title here...', className: 'h3' },
		{ name: 'Heading 4',  openWith: 'h4. ', placeHolder: 'Your title here...', className: 'h4' },
		{ separator: '---------------' },
		{ name: 'Bold',  closeWith: '**', openWith: ' **', className: 'bold', placeHolder: 'Your text here...' },
		{ name: 'Italic',  closeWith: '_', openWith: '_', className: 'italic', placeHolder: 'Your text here...' },
		{ separator: '---------------' },
		{ name: 'Bulleted list', openWith: '*', className: 'list-bullet' },
		{ name: 'Numeric list', openWith: function (markItUp) {
		    var value = "";
		    for (var i = 0; i < markItUp.line; i++) {
		        value += "#"
		    }
		    return value;
		}
        , className: 'list-numeric'
		},
		{ separator: '---------------' },
		{ name: 'Picture', openWith: '[* ', closeWith: ' (!(.([![Alt text]!]))!) *]', placeHolder: '[![Url:!:http://]!]', className: 'image' },
		{ name: 'Link', openWith: '"', closeWith: '":[![Url:!:http://]!]', placeHolder: 'Your text to link...', className: 'link' },
		{ separator: '---------------' },
		{ name: 'Quotes', openWith: '> ', className: 'quotes' },
		{ name: 'Code block/Code in-line', openWith: '(!(<pre class="brush:[![Language:!:html]!]|!|`)!)">\n', closeWith: '(!(</pre>|!|`)!)\n', className: 'code' },
 		{ separator: '---------------' },
	]
}

miu = {
	texyTitle: function (markItUp, char) {
		heading = '';
		n = $.trim(markItUp.selection || markItUp.placeHolder).length;
		for(i = 0; i < n; i++)	{
			heading += char;	
		}
		return '\n'+heading;
	}
}



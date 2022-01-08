(function(){
  if (!document.querySelectorAll) { return; }

  var data = {
    dolar: {rate: '5,19', background: '26A65B', name: 'DÃ³lar'},
    euro: {rate: '6,12', background: '4183D7', name: 'Euro'},
    ouro: {rate: '329,91', background: 'DEB510', name: 'Ouro'},
    bitcoin: {rate: '58916,84', background: 'E87E04', name: 'Bitcoin'}
  };

  var loadButton = function(el) {
    var button = el;
    var buttonLoaded = button.getAttribute('data-loaded');
    var buttonCurrency = button.getAttribute('data-currency');

    if (buttonLoaded) { return; }

    var buttonCurrencyProperties = data[buttonCurrency];

    var tpl = '<span style="background: #{background}; border-radius: 2px; display: inline-block; color: #fff; font-family: sans-serif; font-size: 11px; font-weight: lighter; line-height: 1.4em; letter-spacing: 0.04rem; position: relative; margin: 0; padding: 4px 7px 4px 8px">';
    tpl += '    <span style="margin: 0; padding: 0">{name}</span>';
    if (el.href.match(/^https?:\/\/dolarhoje.com\//)) {
        tpl += '    <span style="margin: 0 0 0 12px; padding: 0";font-size: 15px; >R$ {rate}</span>';
    }
    tpl += '</span>';

    var gen = tpl.replace(/{name}/, buttonCurrencyProperties['name']);
    gen = gen.replace(/{background}/, buttonCurrencyProperties['background']);
    gen = gen.replace(/{rate}/, buttonCurrencyProperties['rate']);

    if (el.href.match(/^https?:\/\/dolarhoje.com\//)) {
      var newTitle = "O " + name.replace('D', 'd').replace('E', 'e').replace('O', 'o') + ' hoje vale R$ ' + buttonCurrencyProperties['rate'];
      button.setAttribute('title', newTitle);
    }

    button.innerHTML = gen;
    button.style.textDecoration = 'none';
    button.setAttribute('data-loaded', true);
  };

  document.querySelectorAll('.dolar-hoje-button').forEach(function(el){
    loadButton(el);
  });

})();
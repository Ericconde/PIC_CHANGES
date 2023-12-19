function mascara(o, f) {
    v_obj = o
    v_fun = f
    setTimeout("execmascara()", 1)
}

function execmascara() {
    v_obj.value = v_fun(v_obj.value)
}

function leech(v) {
    v = v.replace(/o/gi, "0")
    v = v.replace(/i/gi, "1")
    v = v.replace(/z/gi, "2")
    v = v.replace(/e/gi, "3")
    v = v.replace(/a/gi, "4")
    v = v.replace(/s/gi, "5")
    v = v.replace(/t/gi, "7")
    v = v.replace(/d/gi, "8")
    v = v.replace(/d/gi, "9")
    return v
}

function soNumeros(v) {
    return v.replace(/\D/g, "")
}

function telefone(v) {
    v = v.replace(/\D/g, "");             //Remove tudo o que não é dígito
    v = v.replace(/^(\d{2})(\d)/g, "($1) $2"); //Coloca parênteses em volta dos dois primeiros dígitos
    v = v.replace(/(\d)(\d{4})$/, "$1-$2");    //Coloca hífen entre o quarto e o quinto dígitos
    return v;
}

function cpf(v) {
    v = v.replace(/\D/g, "")                    //Remove tudo o que não é dígito
    v = v.replace(/(\d{3})(\d)/, "$1.$2")       //Coloca um ponto entre o terceiro e o quarto dígitos
    v = v.replace(/(\d{3})(\d)/, "$1.$2")       //Coloca um ponto entre o terceiro e o quarto dígitos
    //de novo (para o segundo bloco de números)
    v = v.replace(/(\d{3})(\d{1,2})$/, "$1-$2") //Coloca um hífen entre o terceiro e o quarto dígitos
    return v
}

function cep(v) {
    v = v.replace(/D/g, "")                //Remove tudo o que não é dígito
    v = v.replace(/^(\d{5})(\d)/, "$1-$2") //Esse é tão fácil que não merece explicações
    return v
}

function cnpj(v) {
    v = v.replace(/\D/g, "")                           //Remove tudo o que não é dígito
    v = v.replace(/^(\d{2})(\d)/, "$1.$2")             //Coloca ponto entre o segundo e o terceiro dígitos
    v = v.replace(/^(\d{2})\.(\d{3})(\d)/, "$1.$2.$3") //Coloca ponto entre o quinto e o sexto dígitos
    v = v.replace(/\.(\d{3})(\d)/, ".$1/$2")           //Coloca uma barra entre o oitavo e o nono dígitos
    v = v.replace(/(\d{4})(\d)/, "$1-$2")              //Coloca um hífen depois do bloco de quatro dígitos
    return v
}

function romanos(v) {
    v = v.toUpperCase()             //Maiúsculas
    v = v.replace(/[^IVXLCDM]/g, "") //Remove tudo o que não for I, V, X, L, C, D ou M
    //Essa é complicada! Copiei daqui: http://www.diveintopython.org/refactoring/refactoring.html
    while (v.replace(/^M{0,4}(CM|CD|D?C{0,3})(XC|XL|L?X{0,3})(IX|IV|V?I{0,3})$/, "") != "")
        v = v.replace(/.$/, "")
    return v
}

function site(v) {
    //Esse sem comentarios para que você entenda sozinho ;-)
    v = v.replace(/^http:\/\/?/, "")
    dominio = v
    caminho = ""
    if (v.indexOf("/") > -1)
        dominio = v.split("/")[0]
    caminho = v.replace(/[^\/]*/, "")
    dominio = dominio.replace(/[^\w\.\+-:@]/g, "")
    caminho = caminho.replace(/[^\w\d\+-@:\?&=%\(\)\.]/g, "")
    caminho = caminho.replace(/([\?&])=/, "$1")
    if (caminho != "") dominio = dominio.replace(/\.+$/, "")
    v = "http://" + dominio + caminho
    return v
}

//Paulo César Dias da Silva - 17-07-2012 - 14:38
function data(v) {
    v = v.replace(/\D/g, "")                    //Remove tudo o que não é dígito
    v = v.replace(/(\d{2})(\d)/, "$1/$2")       //Coloca uma barra entre o segundo e o Terceiro dígitos
    v = v.replace(/(\d{2})(\d)/, "$1/$2")       //Coloca uma barra entre o segundo e o Terceiro dígitos
    //de novo (para o segundo bloco de números)
    return v
}

function validaNumero(v) {
  var tecla = event.keyCode;
  if ((tecla == 44 || tecla == 46) || (tecla > 47 && tecla < 58)) // numeros de 0 a 9
     return true;
    else {
        if (tecla != 8) // espaço
           event.keyCode = 0; //return false;
        else
           return true;
    }
}

function moeda(v) {
    v = v.replace(/\D/g, "")  //permite digitar apenas números
    v = v.replace(/[0-9]{12}/, "inválido")   //limita pra máximo 999.999.999,99
    v = v.replace(/(\d{1})(\d{8})$/, "$1.$2")  //coloca ponto antes dos últimos 8 digitos
    v = v.replace(/(\d{1})(\d{5})$/, "$1.$2")  //coloca ponto antes dos últimos 5 digitos
    v = v.replace(/(\d{1})(\d{1,2})$/, "$1,$2")	//coloca virgula antes dos últimos 2 digitos
    return v;
}

function valida_horas(edit) {
    if (event.keyCode < 48 || event.keyCode > 57) {
        event.returnValue = false;
    }
    if (edit.value.length == 2) {
        edit.value += ":";
    }

    if (edit.value.length == 5) {
        if (!verifica_hora(edit)) {
            alert("Hora inválida");
            edit.value = "";
        }
    }
}
function verifica_hora(edit) {
    hrs = (edit.value.substring(0, 2));
    min = (edit.value.substring(3, 5));

    var bCheck = true;

    if ((hrs < 00) || (hrs > 23) || (min < 00) || (min > 59)) {
        bCheck = false;
    }

    if (edit.value == "") {
        bCheck = false;
    }

    return bCheck;
}
/*
  <input type="text" name="hora" OnKeyUp="valida_horas(this)" maxlength="5"> hh:mm<br> 
*/

/*
<input type="text" name="hora" onblur="validarData(this, this.value)" maxlength="5"> hh:mm<br> 
*/
function validarData(campo, valor) {
    var date = valor;
    var ardt = new Array;
    var ExpReg = new RegExp("(0[1-9]|[12][0-9]|3[01])/(0[1-9]|1[012])/[12][0-9]{3}");
    ardt = date.split("/");
    erro = false;
    if (date.search(ExpReg) == -1) {
        erro = true;
    }
    else if (((ardt[1] == 4) || (ardt[1] == 6) || (ardt[1] == 9) || (ardt[1] == 11)) && (ardt[0] > 30))
        erro = true;
    else if (ardt[1] == 2) {
        if ((ardt[0] > 28) && ((ardt[2] % 4) != 0))
            erro = true;
        if ((ardt[0] > 29) && ((ardt[2] % 4) == 0))
            erro = true;
    }

    if (valor != "") {
        if (erro) {
            alert("Atenção, \"" + valor + "\" não é uma data válida.");
            campo.focus();
            campo.value = "";
            return false;
        }
    }
    return true;
}


// LIMITE DE CARACTERES
function maxlength(obj, max) {
    if (obj.value.length >= max - 2) {
        alert("Atenção, você já atingiu o limite de caracteres para este campo");
        obj.value = obj.value.substring(0, max - 1);
        return false;
    }
}


//VINICIUS - 27/08/2013
// Formata hora no padrao HH:MM
function Mascara_Hora(Hora, campo){  
   var hora01 = '';  
   hora01 = hora01 + Hora;  
   if (hora01.length == 2){  
      hora01 = hora01 + ':';  
      campo.value = hora01;  
   }  
   if (hora01.length == 5){  
      Verifica_Hora(campo);  
   }  
}  
  
function Verifica_Hora(campo){  
   hrs = (campo.value.substring(0,2));  
   min = (campo.value.substring(3,5));  
   estado = "";  
   if ((hrs < 00 ) || (hrs > 23) || ( min < 00) ||( min > 59)){  
      estado = "errada";  
   }  
  
   if (campo.value == "") {  
      estado = "errada";  
   }  
   if (estado == "errada") {  
      alert("Hora invalida!");
      campo.focus();
      campo.value = "";
      return false;
  }
  return true;
}

function decimal(v) {
    v = v.replace(/\D/g, "")  //permite digitar apenas números
    v = v.replace(/[0-9]{10}/, "inválido")   //limita    
    v = v.replace(/(\d{1})(\d{2})$/, "$1,$2")  //coloca vírgula antes dos últimos 5 digitos
    return v;
}
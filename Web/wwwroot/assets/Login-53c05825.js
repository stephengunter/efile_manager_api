import{c as p,r as w,e as R,u as S,_ as $}from"./index-7942b0ec.js";import{u as I,a as U,b as C,r as q,d as x,o as B,e as O,h as T,w as a,k as l,b3 as L,g as s,c0 as M,c1 as N,j as _,q as k,l as Q,v as A,b1 as D,av as F,c2 as G,b8 as W,b9 as j,aW as J,b6 as H,a6 as z}from"./index-c3e40adb.js";import{V as c}from"./validate.types-512aac96.js";import{V as K,a as f}from"./VRow-39a21267.js";import{V as h}from"./VTextField-e1b68920.js";import"./VAlert-839ebed3.js";const P=_("h2",{class:"ma-2"},"登入",-1),te={__name:"Login",setup(X){const d=I(),i=U(),m=C(),e=q(x({returnUrl:"",returnQuery:"",form:{username:"",password:""},password:{visible:!1}})),n={username:"Email",password:"密碼"},V={username:{required:p.withMessage(c.REQUIRED(n.username),w),email:p.withMessage(c.WRONG_FORMAT_OF(n.username),R)},password:{required:p.withMessage(c.REQUIRED(n.password),w)}},u=S(V,e.form);B(()=>{if(i.query){e.returnUrl=i.query.returnUrl?i.query.returnUrl:"";let o=JSON.parse(JSON.stringify(i.query));delete o.returnUrl,e.returnQuery=o}});function b(){u.value.$validate().then(o=>{o&&d.dispatch(G,e.form).then(()=>y()).catch(r=>g(r))})}function g(o){let r=W(o);r?d.commit(j,Object.values(r)):J(o)}function y(){H("登入成功"),e.returnUrl?e.returnQuery?m.push({path:e.returnUrl,query:e.returnQuery}):m.push({path:e.returnUrl}):m.push({path:"/"})}function v(){d.commit(z)}return(o,r)=>{const E=$;return O(),T(L,{"max-width":l(F).S},{default:a(()=>[s(M,{class:"font-weight-black"},{default:a(()=>[P]),_:1}),s(D,null,{default:a(()=>[s(N,null,{default:a(()=>[_("form",{onSubmit:A(b,["prevent"]),onInput:v},[s(K,null,{default:a(()=>[s(f,{cols:"12"},{default:a(()=>[s(h,{variant:"outlined",label:n.username,"prepend-inner-icon":"mdi-email-outline",density:"compact",placeholder:`請輸入${n.username}`,modelValue:e.form.username,"onUpdate:modelValue":r[0]||(r[0]=t=>e.form.username=t),"error-messages":l(u).username.$errors.map(t=>t.$message),onInput:l(u).username.$touch,onBlur:l(u).username.$touch},null,8,["label","placeholder","modelValue","error-messages","onInput","onBlur"]),s(h,{variant:"outlined",label:n.password,"prepend-inner-icon":"mdi-lock-outline",density:"compact",placeholder:`請輸入${n.password}`,"append-inner-icon":e.password.visible?"mdi-eye-off":"mdi-eye",type:e.password.visible?"text":"password",modelValue:e.form.password,"onUpdate:modelValue":r[1]||(r[1]=t=>e.form.password=t),"error-messages":l(u).password.$errors.map(t=>t.$message),onInput:l(u).password.$touch,onBlur:l(u).password.$touch,"onClick:appendInner":r[2]||(r[2]=t=>e.password.visible=!e.password.visible)},null,8,["label","placeholder","append-inner-icon","type","modelValue","error-messages","onInput","onBlur"])]),_:1}),s(f,{cols:"12"},{default:a(()=>[s(k,{type:"submit",color:"success",class:"float-right"},{default:a(()=>[Q(" 登入 ")]),_:1})]),_:1}),s(f,{cols:"12"},{default:a(()=>[s(E)]),_:1})]),_:1})],32)]),_:1})]),_:1})]),_:1},8,["max-width"])}}};export{te as default};
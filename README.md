# IPTViewr: Decodificador virtual movistar+ (Movistar TV) #

**IPTViewr** (contracción de visor de televisión IP, en inglés) es un decodificador virtual IPTV para ver los canales de movistar+ España (imagenio) en Windows (Vista, 7, 8.1 o 10).

IPTViewr emula, parcialmente, el funcionamiento del decodificador físico de movistar+. El propósito de este proyecto es llegar a cubrir el 95% de la funcionalidad del decodificador.

La version actual, [v1.5 ‘Kruger 60’ beta 1 SP1][Kruger-60], dispone de las siguientes funcionalidades:
- Lista completa de canales
  - Se muestran todos los canales disponibles, con independencia de si forman parte o no del programa contratado.
  - Sólo pueden visualizarse los canales contratados.
- Numeración de los canales
  - El número de cada canal se ‘calcula’ en base a los paquetes más populares.
- Múltiples formas de visualizar y ordenar la lista (por nombre, número, calidad, etc).
- Grabación de programas en local.
  - La grabación funciona como un ‘vídeo’ convencional. Se indica el canal, hora de comienzo y fin, patrón de repetición y descripción de la grabación.
- Otras funcionalidades menores

Funcionalidades más comunes del decodificador no implementadas:
- Guía electrónica de programas (EPG)
  - movistar+ utiliza un formato propietario no estandarizado para enviar la información de la EPG
  - Salvo con ayuda de la comunidad, la implementación completa de esta funcionalidad puede que nunca sea factible.
- Autoconfiguración de demarcación y de paquetes contratados
- Grabaciones ‘en la nube’
  - Tanto la programación de grabaciones, como la visualización de grabaciones ya realizadas
- Búsqueda de contenidos (función buscar del menú Movistar) 
- Soporte de VOD (vídeo bajo demanda, alquiler de películas, etc)
  - Esta funcionalidad no está previsto implementarla

## Información importante ##

Este software **SE PROPORCIONA "TAL CUAL", SIN GARANTÍA DE NINGÚN TIPO**, expresa o implícita, incluyendo (pero no limitándose a) las garantías de comerciabilidad, idoneidad para un propósito particular y no infracción.  
Puede dejar de funcionar, de forma parcial o total, en cualquier momento si Telefónica modifica la configuración del servicio de movistar+.  
En ningún caso, los autores o titulares del Copyright serán responsables de ninguna reclamación, daños o cualquier otra responsabilidad, ya sea en una acción de contrato, agravio o cualquier otro motivo, con o sin relación con el software o el uso u otros aspectos del software.

El presente software **NO ESTÁ SOPORTADO NI RESPALDADO** por Movistar ni por Telefónica. Telefónica de España no tiene responsabilidad alguna si hay canales (incluso si están contratados) que no pueden verse en el PC.
**NO DEBE LLAMARSE NUNCA al 1002 o al 1004** si desaparecen canales de la lista o no pueden verse o cualquier otra anomalía. Telefónica sólo proporciona ayuda y soporte técnico para su decodificador físico y sólo para los servicios contratados.

Todas las marcas comerciales, marcas de servicio, nombres comerciales, nombres de productos y logotipos son propiedad de sus respectivos propietarios, incluyendo en algunos casos el Grupo Telefónica.  
Su uso en el contexto de este software no constituye un respaldo ni apoyo por parte de sus respectivos propietarios.

## Licencia de uso y acceso al código ##

IPTViewr es un proyecto de código abierto, regido por la “Licencia Recíproca de Microsoft” (MS-Rl), según se describe en los archivos [LICENSE_ES][] y [LICENSE][].  
Cualquiera puede tener acceso al código fuente, proponer mejoras y utilizar parte(s) del código para sus propios fines.  
No obstante, y de acuerdo con la licencia, el uso del código en otros programas requiere atribuación.
Así mismo, cualquier cambio en el código por terceros conlleva la obligación de hacer público dicho código modificado.

Este proyecto estaba alojado previamente en el defunto [movistartv.codeplex.com][codeplex].
El código fuente ya está migrado a GitHub. Y se ha creado un nuevo portal de entrada [alphacentaury.org/movistartv/][alphacentaury].
De manera progresiva se va a proceder a migrar toda la documentación existente al nuevo portal.  
Además, el nuevo portal alojará los archivos de instalación.

[LICENSE]: LICENSE
[LICENSE_ES]: LICENSE_ES
[codeplex]: https://movistartv.codeplex.com
[Kruger-60]: https://www.alphacentaury.org/temas/movistartv/downloads/
[alphacentaury]: https://www.alphacentaury.org/movistartv/

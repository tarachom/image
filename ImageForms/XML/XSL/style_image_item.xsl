<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="2.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns="http://www.w3.org/1999/xhtml">

  <xsl:output method="text" indent="yes" />

  <xsl:param name="table" />

  <xsl:template match="shema"><![CDATA[<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns="http://www.w3.org/1999/xhtml">
<xsl:output method="html" indent="yes" />

   <xsl:template match="root">
 
   <html xmlns="http://www.w3.org/1999/xhtml">
      <head>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <link rel="stylesheet" href="/IMAGE/css/style.css" type="text/css" />
        <title>
          <xsl:text>Образи</xsl:text>
        </title>
      </head>
      <body>       
       
       ]]><xsl:for-each select="image/table[@id=$table]"><![CDATA[ 
       
       <h2>Елемент: ]]><xsl:value-of select="@name"/><![CDATA[</h2>
       <p><a href="../../">Головна</a> | <a href="../">Список ]]><xsl:value-of select="@name"/><![CDATA[</a></p>
       
       <form method="post" enctype="multipart/form-data">
            <input type="hidden" name="state" value="send"/>
            
            <table class="fields" width="100%" border="0" cellpadding="2" cellspacing="5">
              <col width="15%"/>
              <col width="60%"/>
              <col width="25%"/>
              
              ]]><xsl:for-each select="field[@code != 'ID']"><![CDATA[
              
              <tr>
                  <td class="left_col">]]><xsl:value-of select="@name"/><![CDATA[:</td>
                  <td>
                     <input class="input_text" type="text" name="field_]]><xsl:value-of select="@code"/><![CDATA[" />
                  </td>
                  <td>]]><xsl:value-of select="@type"/><![CDATA[</td>
              </tr>
              
              ]]></xsl:for-each><![CDATA[
              
              <tr>
                  <td></td>
                  <td>
                     <input type="submit" value="Додати" />
                  </td>
                  <td></td>
              </tr>
              
           </table>
           
       </form>
              
       ]]></xsl:for-each><![CDATA[
       
      </body>
    </html>

   </xsl:template>
</xsl:stylesheet>
    ]]>
  
  </xsl:template>

</xsl:stylesheet>
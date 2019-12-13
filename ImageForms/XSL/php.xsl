<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="2.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns="http://www.w3.org/1999/xhtml">

  <xsl:output method="text" indent="yes" />

  <xsl:param name="table" />

  <xsl:template match="root">
    <![CDATA[<?php
  
define("ROOT_PATH", "../../");

define("TEMPLATE", ROOT_PATH . "include/template/i/]]><xsl:value-of select="$table"/><![CDATA[/index.xsl");
//define("XMLSHEMA", ROOT_PATH . "include/model.xml");

include(ROOT_PATH . "include/include.php");

//Масив запитів
$sqlArray = array();

//ХМЛ дані
$xmlData = "";

]]>

    <xsl:for-each select="table[@id =$table]">

      $form_state = isset($_POST["state"]) ? $_POST["state"] : "";
      if ($form_state == "send") {

      //Список полів
      <xsl:for-each select="field"> 
          //
          // Поле: <xsl:value-of select="@name"/>
          //
          <xsl:choose>
              <xsl:when test="@type = 'link'">
                  $post_field_<xsl:value-of select="@code"/> = isset($_POST["field_<xsl:value-of select="@code"/>"]) ? $_POST["field_<xsl:value-of select="@code"/>"] : "";
                  $field_<xsl:value-of select="@code"/> = GetOrInsertElement("<xsl:value-of select="@table_link"/>", $post_field_<xsl:value-of select="@code"/>);
              </xsl:when>
              <xsl:otherwise>
                  $field_<xsl:value-of select="@code"/> = isset($_POST["field_<xsl:value-of select="@code"/>"]) ? $mysqli->real_escape_string($_POST["field_<xsl:value-of select="@code"/>"]) : "";
              </xsl:otherwise>
          </xsl:choose>
        </xsl:for-each>

        //
        // Запис в базу
        //
      
        $sql = "INSERT INTO `<xsl:value-of select="@code"/>` (
        <xsl:for-each select="field">
          <xsl:if test="position() > 1">,</xsl:if>
          `<xsl:value-of select="@code"/>`
        </xsl:for-each>
        ) VALUES ("
        <xsl:for-each select="field">
          <xsl:if test="position() > 1">","</xsl:if>
            <xsl:choose>
              <xsl:when test="@type = 'link'">
                   . $field_<xsl:value-of select="@code"/> .
              </xsl:when>
              <xsl:otherwise>
                   . "'" . $field_<xsl:value-of select="@code"/> . "'" .
              </xsl:otherwise>
           </xsl:choose>
        </xsl:for-each>
        ")";

        if (!$result = $mysqli->query($sql)) {
            echo "Запрос: " . $sql . "\n";
            echo "Номер_ошибки: " . $mysqli->errno . "\n";
            echo "Ошибка: " . $mysqli->error . "\n";
            exit;
        }
    }
        
    $sqlArray["list"] = "
        SELECT 
        <xsl:for-each select="field">
          <xsl:if test="position() > 1">,</xsl:if>
          <xsl:choose>
              <xsl:when test="@type = 'link'">
                  `<xsl:value-of select="@table_link"/>`.`NAME` as `<xsl:value-of select="@code"/>`
              </xsl:when>
              <xsl:otherwise>
                   `<xsl:value-of select="../@code"/>`.`<xsl:value-of select="@code"/>`
              </xsl:otherwise>
           </xsl:choose>
        </xsl:for-each>
        
        FROM `<xsl:value-of select="@code"/>`
        
        <xsl:for-each select="field">
            <xsl:if test="@type = 'link'">
            LEFT JOIN `<xsl:value-of select="@table_link"/>` ON `<xsl:value-of select="@table_link"/>`.`ID` =  `<xsl:value-of select="../@code"/>`.`<xsl:value-of select="@code"/>`
            </xsl:if>
        </xsl:for-each>
    ";
    
    </xsl:for-each>
    
    <![CDATA[

$xmlData = GenerateXML($sqlArray, $xmlData);
echo GenerateHTML(false, $xmlData, TEMPLATE);

?>]]>


  </xsl:template>

</xsl:stylesheet>
<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns="http://www.w3.org/1999/xhtml">
  <xsl:output method="text" indent="no" />

  <!-- ІД таблиці -->
  <xsl:param name="table" />
  
  <!-- ІД родітельської таблиці -->
  <xsl:param name="parent_table_id" />
  
  <xsl:template match="shema">
    <![CDATA[<?php
  
include("../../include/include.php");

//Масив запитів
$sqlArray = array();

//ХМЛ дані
$xmlData = "";

//Список параметрів
$xslParams = array();

]]>

    <xsl:for-each select="image/table[@id=$table]">

      //Родітельський запис
      $parent_id = isset($_GET["e"]) ? (int)$_GET["e"] : 0;
      $xslParams["parent_id"] = $parent_id;

      $form_state = isset($_POST["state"]) ? $_POST["state"] : "";
      if ($form_state == "send") {
          
           //Поле ObjectID
           $object_id = isset($_POST["object_id"]) ? (int)$_POST["object_id"] : 0;
           
           <xsl:for-each select="field[@code != 'ID' and @code != 'PARENTID' and @code != 'OBJECTID']">
             //Поле: <xsl:value-of select="@name"/>
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

           // Запис в базу
           $sql = "
           INSERT INTO `<xsl:value-of select="@code"/>`
           (`PARENTID`, `OBJECTID`
           <xsl:for-each select="field[@code != 'ID' and @code != 'PARENTID' and @code != 'OBJECTID']">
             <xsl:text>,`</xsl:text><xsl:value-of select="@code"/><xsl:text>`</xsl:text>     
           </xsl:for-each>
           ) VALUES ($parent_id, $object_id
            <xsl:for-each select="field[@code != 'ID' and @code != 'PARENTID' and @code != 'OBJECTID']">
             <xsl:text>, </xsl:text>
             <xsl:choose>
                <xsl:when test="@type = 'link'">
                      <xsl:text>$field_</xsl:text><xsl:value-of select="@code"/>
                </xsl:when>
                <xsl:otherwise>
                      <xsl:text>'" . $field_</xsl:text><xsl:value-of select="@code"/><xsl:text> . "'</xsl:text>      
                </xsl:otherwise>
             </xsl:choose>      
           </xsl:for-each> 
           )";
           
           if (!$result = $mysqli->query($sql)) {
              echo "Запрос: " . $sql . "\n";
              echo "Номер_ошибки: " . $mysqli->errno . "\n";
              echo "Ошибка: " . $mysqli->error . "\n";
              exit;
          }
      }
      
      //Вибірка таблиці <xsl:value-of select="field[@code = 'OBJECTID']/@table_link"/> для списку
      $sqlArray["object_id"] = "SELECT `ID`, `NAME` FROM `<xsl:value-of select="field[@code = 'OBJECTID']/@table_link"/>`";
      
    </xsl:for-each>

    <![CDATA[

$xmlData = GenerateXML($sqlArray, $xmlData);
echo GenerateHTML(false, $xmlData, "index.xsl", $xslParams);

?>]]>

  </xsl:template>
</xsl:stylesheet>
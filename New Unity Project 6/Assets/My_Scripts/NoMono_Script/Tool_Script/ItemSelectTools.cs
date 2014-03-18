using UnityEngine;
using System.Collections;

public class ItemTools
{
	//ID为物品ID
	public static NeedsNode GetNeeds(int ID)
	{
		System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
		NeedsNode result = null;
		
		try
		{
			doc.Load("Assets/Datas/Items.xml");
		}
		catch (System.Exception ex)
		{
			return null;
		}
		
		System.Xml.XmlNode targetItem = null;//ID物品
		//遍历物品数据库
		foreach (System.Xml.XmlNode node in doc.DocumentElement.ChildNodes)
		{
			if (node.ChildNodes[0].InnerXml == ID.ToString())
			{
				targetItem = node;
				break;
			}
		}
		
		if (targetItem != null)
		{
			System.Xml.XmlNode needsNode = targetItem.SelectSingleNode("needs");
			System.Xml.XmlNodeList need1NodeList = needsNode.SelectNodes("need1");
			System.Xml.XmlNodeList need2NodeList = needsNode.SelectNodes("need2");
			
			result = new NeedsNode(new NeedNode[need1NodeList.Count], new NeedNode[need2NodeList.Count]);
			
			//解析Need1部分
			for (int i = 0; i < need1NodeList.Count; i++)
			{
				int temp_ID = int.Parse(need1NodeList[i].SelectSingleNode("ID").InnerText);
				int temp_count = int.Parse(need1NodeList[i].SelectSingleNode("count").InnerText);
				bool temp_flag=false;
				if(int.Parse(need1NodeList[i].SelectSingleNode("flag").InnerText)==1)
					temp_flag=true;
				result.Need1[i] = new NeedNode(temp_ID,temp_count,temp_flag);
			}
			
			//解析Need2部分
			for (int i = 0; i < need2NodeList.Count; i++)
			{
				int temp_id = int .Parse((need2NodeList[i].SelectSingleNode("ID").InnerText));
				int temp_count = int.Parse(need2NodeList[i].SelectSingleNode("count").InnerText);
				bool temp_flag = false;
				if (int.Parse(need2NodeList[i].SelectSingleNode("flag").InnerText) == 1)
					temp_flag = true;
				result.Need2[i] = new NeedNode(temp_id, temp_count, temp_flag);
			}
		}
		
		return result;
	}
}

public class NeedsNode
{
	public NeedNode[] Need1 { get; set; }
	public NeedNode[] Need2 { get; set; }
	
	public NeedsNode(NeedNode[] need1,NeedNode[] need2)
	{
		this.Need1 = need1;
		this.Need2 = need2;
	}
	
	public override string ToString()
	{
		string result="Need1:\n";
		
		foreach (NeedNode node in this.Need1)
		{
			result+="ID="+node.ID.ToString()+"\tcount="+node.Count.ToString()+"\tflag="+node.Flag.ToString()+"\n";
		}
		result += "Need2:\n";
		
		foreach (NeedNode node in this.Need2)
		{
			result += "ID=" + node.ID.ToString() + "\tcount=" + node.Count.ToString() + "\tflag=" + node.Flag.ToString() + "\n";
		}
		
		return result;
	}
}

public class NeedNode
{
	public int ID { get; set; }//物品或资源ID
	public int Count { get; set; }//需要数量
	public bool Flag { get; set; }//是否被消耗
	
	public NeedNode(int ID,int count,bool flag)
	{
		this.ID = ID;
		this.Count = count;
		this.Flag = flag;
	}
}
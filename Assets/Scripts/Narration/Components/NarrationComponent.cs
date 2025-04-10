using System;
using System.Collections;
using System.Collections.Generic;
using Kuchinashi;
using Kuchinashi.Utils.Progressable;
using Phosphorescence.DataSystem;
using Phosphorescence.Narration.Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Phosphorescence.Narration
{
    public abstract class NarrationComponent : MonoBehaviour
    {
        public CanvasGroupAlphaProgressable CanvasGroup;
    }
}